using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NRules;
using Rule.WebAPI.Context;
using Rule.WebAPI.Model.DTO;
using Rule.WebAPI.Services.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rule.WebAPI.Services
{
    public class RuleData : IRuleData
    {
        private CustomRuleRepository _customRuleRepository;
        private readonly RuleDbContext _ruleDbContext;
        private readonly IMapper _mapper;
        public RuleData(RuleDbContext ruleDbContext, IMapper mapper)
        {
            _customRuleRepository = new CustomRuleRepository();
            _ruleDbContext = ruleDbContext;
            _mapper = mapper;
        }

        public async Task<bool> SaveRule(RuleEngineRequestModel ruleEngineRequestModel)
        {
            ruleEngineRequestModel.Rules.ForEach(rule =>
            {
                var dbRule = _mapper.Map<Model.RuleEngine>(rule);
                dbRule.NRule = new NRule() { Name = ruleEngineRequestModel.RuleName };
                _ruleDbContext.RuleEngines.Add(dbRule);
            });

            return await _ruleDbContext.SaveChangesAsync() > 0;
        }

        public async Task<NRuleResponse> GetRule(int ruleId)
        {
            return await _mapper.ProjectTo<NRuleResponse>(_ruleDbContext.NRules.Where(x => x.Id == ruleId)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<NRuleResponse>> GetRules()
        {
            return await _mapper.ProjectTo<NRuleResponse>(_ruleDbContext.NRules).ToListAsync();
        }

        public async Task<IEnumerable<FilterOperationResponse>> GetOperations()
        {
            return await _mapper.ProjectTo<FilterOperationResponse>(_ruleDbContext.Operations).ToListAsync();
        }

        public async Task<bool> UpdateRule(RuleEngineRequestModel ruleEngineRequestModel)
        {
            var nRule = await _ruleDbContext.NRules.FirstOrDefaultAsync(x => x.Id == ruleEngineRequestModel.Id);
            if (nRule == null)
                return false;
            nRule.Name = ruleEngineRequestModel.RuleName;

            ruleEngineRequestModel.Rules.ForEach(rule =>
            {
                var dbRule = _ruleDbContext.RuleEngines.FirstOrDefault(x => x.Id == rule.Id);
                if (dbRule != null)
                    _ruleDbContext.RuleEngines.Update(_mapper.Map(rule, dbRule));
            });

            return await _ruleDbContext.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<PersonRequestModel>> Execute(List<RuleEngineEntity> ruleEngineEntities)
        {
            if (!ruleEngineEntities.Any())
                return null;

            return await CompileRule(new RuleEngineRequestModel()
            {
                RuleName = "Default",
                Rules = ruleEngineEntities.ToList()
            });
        }

        public async Task<IEnumerable<PersonRequestModel>> ExecuteByRuleId(int ruleId)
        {
            var ruleEngines = _mapper.Map<IEnumerable<RuleEngineEntity>>(await _ruleDbContext.RuleEngines.Where(x => x.NRuleId == ruleId).ToListAsync());

            if (!ruleEngines.Any())
                return null;

            return await CompileRule(new RuleEngineRequestModel()
            {
                RuleName = "Default",
                Rules = ruleEngines.ToList()
            });
        }

        private async Task<IEnumerable<PersonRequestModel>> CompileRule(RuleEngineRequestModel ruleEngineRequestModel)
        {
            List<PersonRequestModel> people = new List<PersonRequestModel>();
            _customRuleRepository.LoadRules<PersonRuleRequestModel>(ruleEngineRequestModel);

            var factory = _customRuleRepository.Compile();

            var persons = _ruleDbContext.Persons.ToList();
            var session = factory.CreateSession();
            foreach (var person in persons)
            {
                session.Insert(new PersonRuleRequestModel() 
                {
                    Person = new PersonRequestModel()
                    {
                        Age = person.Age,
                        BirthDate = person.BirthDate,
                        IsMale = person.IsMale,
                        Name = person.Name.ToLower(),
                        Id = person.Id
                    }
                });
                if (session.Fire() == 1)
                    people.Add(_mapper.Map<PersonRequestModel>(person));
            }
            return people;
        }

    }
}
