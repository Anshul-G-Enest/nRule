using Rule.WebAPI.Model.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rule.WebAPI.Services.Interface
{
    public interface IRuleData
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ruleEngineRequestModel">Save Rule</param>
        /// <returns></returns>
        Task<bool> SaveRule(RuleEngineRequestModel ruleEngineRequestModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ruleId">GetRule</param>
        /// <returns></returns>
        Task<NRuleResponse> GetRule(int ruleId);

        /// <summary>
        /// Get Rules
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<NRuleResponse>> GetRules();

        /// <summary>
        /// Get Operations
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<FilterOperationResponse>> GetOperations();

        /// <summary>
        /// Update Rule
        /// </summary>
        /// <param name="ruleEngineRequestModel"></param>
        /// <returns></returns>
        Task<bool> UpdateRule(RuleEngineRequestModel ruleEngineRequestModel);
    }
}
