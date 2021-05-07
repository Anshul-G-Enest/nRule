using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NRules;
using Rule.WebAPI.Context;
using Rule.WebAPI.Model.DTO;
using Rule.WebAPI.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rule.WebAPI.Services
{
    public class ExecuteData : IExecuteData
    {
        private readonly RuleDbContext _ruleDbContext;
        private readonly IMapper _mapper;
        private CustomRuleRepository _customRuleRepository;
        public ExecuteData(RuleDbContext ruleDbContext, IMapper mapper)
        {
            _customRuleRepository = new CustomRuleRepository();
            _ruleDbContext = ruleDbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PersonRequestModel>> ExecuteRuleForQueryBuilder(List<RuleEngineEntity> ruleEngineEntities)
        {
            if (!ruleEngineEntities.Any())
                return null;

            return GetData(CompileRule(new RuleEngineRequestModel()
            {
                RuleName = "Default",
                Rules = ruleEngineEntities.ToList()
            }));
        }

        public async Task<IEnumerable<PersonRequestModel>> ExecuteRuleForQueryBuilderByExistingRuleId(int ruleId)
        {
            var ruleEngines = _mapper.Map<IEnumerable<RuleEngineEntity>>(await _ruleDbContext.RuleEngines.Where(x => x.NRuleId == ruleId).ToListAsync());

            if (!ruleEngines.Any())
                return null;

            return GetData(CompileRule(new RuleEngineRequestModel()
            {
                RuleName = "Default",
                Rules = ruleEngines.ToList()
            }));
        }

        public async Task<bool> ExecuteRuleWithDynamicData(RuleEngineRequestModel ruleEngineRequestModel)
        {
            if (!ruleEngineRequestModel.Rules.Any())
                return false;

            return ExecuteRuleForDynamicJson(CompileRule(ruleEngineRequestModel), ruleEngineRequestModel);
        }

        public async Task<bool> ExecuteRuleWithDynamicDataAndByExistingRuleId(RuleEngineRequestModel ruleEngineRequestModel)
        {
            if (ruleEngineRequestModel.RuleId <= 0)
                return false;

            var ruleEngines = _mapper.Map<IEnumerable<RuleEngineEntity>>(await _ruleDbContext.RuleEngines.Where(x => x.NRuleId == ruleEngineRequestModel.RuleId).ToListAsync());

            if (!ruleEngines.Any())
                return false;

            return ExecuteRuleForDynamicJson(CompileRule(ruleEngineRequestModel), ruleEngineRequestModel);
        }

        public async Task<IEnumerable<EntityTypeResponse>> EntityTypes()
        {
            return _mapper.Map<IEnumerable<EntityTypeResponse>>(await _ruleDbContext.EntityTypes.ToListAsync());
        }

        public List<string> GetFields(int entityId)
        {
            var enumType = (EntityTypeEnum)Enum.ToObject(typeof(EntityTypeEnum), entityId);
            var properties = new List<string>();
            switch (enumType)
            {
                case EntityTypeEnum.Aircraft:
                    properties = typeof(AircraftRequestModel).GetProperties().Select(f => f.Name).ToList();
                    break;
                case EntityTypeEnum.Airport:
                    properties = typeof(AirportRequestModel).GetProperties().Select(f => f.Name).ToList();
                    break;
                case EntityTypeEnum.Country:
                    properties = typeof(CountryRequestModel).GetProperties().Select(f => f.Name).ToList();
                    break;
                case EntityTypeEnum.Person:
                    properties = typeof(PersonRequestModel).GetProperties().Select(f => f.Name).ToList();
                    break;
                case EntityTypeEnum.Trips:
                    properties = typeof(TripRequestModel).GetProperties().Select(f => f.Name).ToList();
                    break;
            }
            return properties;
        }

        #region Private Region

        private IEnumerable<PersonRequestModel> GetData(ISession session)
        {
            List<PersonRequestModel> people = new List<PersonRequestModel>();
            var persons = _ruleDbContext.Persons.ToList();
            foreach (var person in persons)
            {
                session.Insert(new ExecuteRuleRequestModel()
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

        private bool ExecuteRuleForDynamicJson(ISession session, RuleEngineRequestModel ruleEngineRequestModel)
        {
            //List<ExecuteRuleRequestModel> executeRuleRequests = new List<ExecuteRuleRequestModel>();
            ExecuteRuleRequestModel ruleModel = new ExecuteRuleRequestModel();
            foreach (var rule in ruleEngineRequestModel.Rules)
            {
                switch (rule.EntityType)
                {
                    case EntityTypeEnum.Country:
                        ruleModel.Country = JsonConvert.DeserializeObject<CountryRequestModel>(ruleEngineRequestModel.JsonData);
                        break;
                    case EntityTypeEnum.Aircraft:
                        ruleModel.Aircraft = JsonConvert.DeserializeObject<AircraftRequestModel>(ruleEngineRequestModel.JsonData);
                        break;
                    case EntityTypeEnum.Airport:
                        ruleModel.Airport = JsonConvert.DeserializeObject<AirportRequestModel>(ruleEngineRequestModel.JsonData);
                        break;
                    case EntityTypeEnum.Trips:
                        ruleModel.Trip = JsonConvert.DeserializeObject<TripRequestModel>(ruleEngineRequestModel.JsonData);
                        break;
                    case EntityTypeEnum.Person:
                        ruleModel.Person = JsonConvert.DeserializeObject<PersonRequestModel>(ruleEngineRequestModel.JsonData);
                        break;
                }
            }

            session.Insert(ruleModel);
            return session.Fire() == 1;
        }

        private ISession CompileRule(RuleEngineRequestModel ruleEngineRequestModel)
        {
            foreach (var rule in ruleEngineRequestModel.Rules)
            {
                rule.EntityName.PropertyName = rule.EntityType.ToString();
            }
            _customRuleRepository.LoadRules<ExecuteRuleRequestModel>(ruleEngineRequestModel);

            var factory = _customRuleRepository.Compile();

            return factory.CreateSession();
        }

        #endregion

    }
}
