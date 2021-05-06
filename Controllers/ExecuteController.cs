using Microsoft.AspNetCore.Mvc;
using Rule.WebAPI.Model.DTO;
using Rule.WebAPI.Services.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rule.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExecuteController : ControllerBase
    {
        private IExecuteData _executeData;
        public ExecuteController(IExecuteData executeData)
        {
            _executeData = executeData;
        }

        [HttpPost("executeruleforquerybuilder")]
        public async Task<IActionResult> ExecuteRule(List<RuleEngineEntity> ruleEngineEntities)
        {
            return Ok(await _executeData.ExecuteRuleForQueryBuilder(ruleEngineEntities));
        }

        [HttpGet("executeruleforquerybuilderbyexistingruleId/{ruleId}")]
        public async Task<IActionResult> ExecuteRule(int ruleId)
        {
            return Ok(await _executeData.ExecuteRuleForQueryBuilderByExistingRuleId(ruleId));
        }

        [HttpPost("executerulewithdynamicjson")]
        public async Task<IActionResult> ExecuteRule(RuleEngineRequestModel ruleEngineRequestModel)
        {
            return Ok(await _executeData.ExecuteRuleWithDynamicData(ruleEngineRequestModel));
        }

        [HttpPost("executerulefordynamicjsonandbyexistingruleId")]
        public async Task<IActionResult> ExecuteRuleWithDynamicDataAndByExistingRuleId(RuleEngineRequestModel ruleEngineRequestModel)
        {
            return Ok(await _executeData.ExecuteRuleWithDynamicDataAndByExistingRuleId(ruleEngineRequestModel));
        }

        [HttpGet("entityTypes")]
        public async Task<IActionResult> ExecuteRule()
        {
            return Ok(await _executeData.EntityTypes());
        }

        [HttpGet("Fields/{entityId}")]
        public IActionResult GetFields(int entityId)
        {
            return Ok(_executeData.GetFields(entityId));
        }
    }
}
