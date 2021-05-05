using Microsoft.AspNetCore.Mvc;
using Rule.WebAPI.Model.DTO;
using Rule.WebAPI.Services.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rule.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RuleController : ControllerBase
    {
        private IRuleData _ruleData;
        public RuleController(IRuleData ruleData)
        {
            _ruleData = ruleData;
        }

        [HttpPost]
        public async Task<IActionResult> Post(RuleEngineRequestModel ruleEngineRequestModel)
        {
            var isSaved = await _ruleData.SaveRule(ruleEngineRequestModel);
            if (isSaved)
                return Ok(ruleEngineRequestModel);

            return BadRequest("bad request");
        }

        [HttpPut]
        public async Task<IActionResult> Put(RuleEngineRequestModel ruleEngineRequestModel)
        {
            var isSaved = await _ruleData.UpdateRule(ruleEngineRequestModel);
            if (isSaved)
                return Ok(ruleEngineRequestModel);

            return BadRequest("bad request");
        }

        [HttpGet]
        public async Task<IActionResult> Get(int ruleId)
        {
            var rule = await _ruleData.GetRule(ruleId);
            if (rule != null)
                return Ok(rule);

            return NotFound("rule Not found with Id.");
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var rules = await _ruleData.GetRules();
            if (rules.Any())
                return Ok(rules);

            return NotFound();
        }

        [HttpGet("operations")]
        public async Task<IActionResult> Operations()
        {
            var operations = await _ruleData.GetOperations();
            if (operations.Any())
                return Ok(operations);

            return NotFound();
        }

        [HttpPost("ExecuteRule")]
        public async Task<IActionResult> ExecuteRule(List<RuleEngineEntity> ruleEngineEntities)
        {
            return Ok(await _ruleData.Execute(ruleEngineEntities));
        }

        [HttpGet("ExecuteRule/{ruleId}")]
        public async Task<IActionResult> ExecuteRule(int ruleId)
        {
            return Ok(await _ruleData.ExecuteByRuleId(ruleId));
        }
    }
}
