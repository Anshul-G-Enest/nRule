using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NRules;
using Rule.WebAPI.Context;
using Rule.WebAPI.Model;
using Rule.WebAPI.Model.DTO;
using Rule.WebAPI.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Rule.WebAPI.Services
{
    public class PersonData : IPersonData
    {
        private CustomRuleRepository _customRuleRepository;
        private readonly RuleDbContext _ruleDbContext;
        private readonly IMapper _mapper;
        public PersonData(RuleDbContext ruleDbContext, IMapper mapper)
        {
            _customRuleRepository = new CustomRuleRepository();
            _ruleDbContext = ruleDbContext;
            _mapper = mapper;
        }

        public async Task<bool> SavePerson(PersonRequestModel personRequestModel)
        {
            return await AddPerson(personRequestModel);

            //var rules = _mapper.Map<IEnumerable<RuleEngineEntity>>(await _ruleDbContext.Rules.Where(x => personRuleRequestModel.RuleIds.Contains(x.Id)).ToListAsync());

            //if (!rules.Any())
            //    return false;

            //_customRuleRepository.LoadRules<PersonRuleRequestModel>(new RuleEngineRequestModel()
            //{
            //    RuleName = "FirstRule",
            //    Rules = rules.ToList()
            //});

            ////Compile rules
            //var factory = _customRuleRepository.Compile();

            ////Create a working session
            //var session = factory.CreateSession();

            //session.Insert(personRuleRequestModel);

            //var IspassedorNot = session.Fire();

            //if(IspassedorNot > 0)
            //    await AddPerson(personRuleRequestModel.Person);

            //return IspassedorNot > 0;

        }

        private async Task<bool> AddPerson(PersonRequestModel personRequestModel)
        {
            _ruleDbContext.Persons.Add(_mapper.Map<Model.Person>(personRequestModel));

            return await _ruleDbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdatePerson(PersonRequestModel personRequestModel)
        {
            var person = await _ruleDbContext.Persons.FirstOrDefaultAsync(x => x.Id == personRequestModel.Id);
            if (person == null)
                return false;

            _ruleDbContext.Persons.Update(_mapper.Map(personRequestModel, person));

            return await _ruleDbContext.SaveChangesAsync() > 0;
        }

        public async Task<PersonRequestModel> GetPerson(int personId)
        {
            return await _mapper.ProjectTo<PersonRequestModel>(_ruleDbContext.Persons.Where(x => x.Id == personId)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PersonRequestModel>> GetPersons()
        {
            return await _mapper.ProjectTo<PersonRequestModel>(_ruleDbContext.Persons).ToListAsync();
        }

        public List<string> GetFields()
        {
            return typeof(Person).GetProperties().Select(f => f.Name).ToList();
        }
    }
}
