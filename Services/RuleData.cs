using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        private readonly RuleDbContext _ruleDbContext;
        private readonly IMapper _mapper;
        public RuleData(RuleDbContext ruleDbContext, IMapper mapper)
        {
            _ruleDbContext = ruleDbContext;
            _mapper = mapper;
        }

        public async Task<bool> SaveRule(RuleEngineRequestModel ruleEngineRequestModel)
        {
            ruleEngineRequestModel.Rules.ForEach(rule =>
            {
                _ruleDbContext.RuleEngines.Add(_mapper.Map<Model.RuleEngine>(rule));
            });

            _ruleDbContext.NRules.Add(new NRule() { Name = ruleEngineRequestModel.RuleName });

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
            var nRule = await _ruleDbContext.NRules.FirstOrDefaultAsync(x => x.Id == ruleEngineRequestModel.RuleId);
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
    }
}
