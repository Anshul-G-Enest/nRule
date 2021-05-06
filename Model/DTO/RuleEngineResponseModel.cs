using System.Collections.Generic;

namespace Rule.WebAPI.Model.DTO
{
    public class RuleEngineResponseModel
    {
        public int Id { get; set; }
        public FilterStatementConnectorResponse FilterConnector { get; set; }
        public string PropertyName { get; set; }
        public FilterOperationResponse FilterOperation { get; set; }
        public object Value { get; set; }
        public object SecondValue { get; set; }
        public EntityTypeResponse EntityType { get; set; }
    }

    public class FilterStatementConnectorResponse : FilterBase { }

    public class FilterOperationResponse : FilterBase { }

    public class EntityTypeResponse : FilterBase 
    {
        public List<string> Fields { get; set; }
    }

    public class NRuleResponse : FilterBase 
    {
        public List<RuleEngineResponseModel> Rules { get; set; }
    }

    public class FilterBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
