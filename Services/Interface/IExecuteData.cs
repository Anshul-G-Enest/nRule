using Rule.WebAPI.Model.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rule.WebAPI.Services.Interface
{
    public interface IExecuteData
    {
        /// <summary>
        /// Execute Rule For Query Builder
        /// </summary>
        /// <param name="ruleEngineEntities">the ruleEngineEntities.</param>
        /// <returns></returns>
        Task<IEnumerable<PersonRequestModel>> ExecuteRuleForQueryBuilder(List<RuleEngineEntity> ruleEngineEntities);

        /// <summary>
        /// Execute Rule For Query Builder By Existing RuleId
        /// </summary>
        /// <param name="ruleId"></param>
        /// <returns></returns>
        Task<IEnumerable<PersonRequestModel>> ExecuteRuleForQueryBuilderByExistingRuleId(int ruleId);

        /// <summary>
        /// Execute Rule With Dynamic Data
        /// </summary>
        /// <param name="ruleEngineRequestModel"></param>
        /// <returns></returns>
        Task<bool> ExecuteRuleWithDynamicData(RuleEngineRequestModel ruleEngineRequestModel);

        /// <summary>
        /// Execute Rule With Dynamic Data And By Existing RuleId
        /// </summary>
        /// <param name="ruleEngineRequestModel"></param>
        /// <returns></returns>
        Task<bool> ExecuteRuleWithDynamicDataAndByExistingRuleId(RuleEngineRequestModel ruleEngineRequestModel);

        /// <summary>
        /// EntityTypes
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<EntityTypeResponse>> EntityTypes();

        /// <summary>
        /// GetFields
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        List<string> GetFields(int entityId);
    }
}
