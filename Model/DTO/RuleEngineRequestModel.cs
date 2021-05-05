using System.Collections.Generic;

namespace Rule.WebAPI.Model.DTO
{
    public class RuleEngineRequestModel
    {
        public int Id { get; set; }
        public string RuleName { get; set; }
        public List<RuleEngineEntity> Rules { get; set; }
    }
    public class RuleEngineEntity
    {
        public int Id { get; set; }
        public FilterStatementConnector FilterConnector { get; set; }
        public string PropertyName { get; set; }
        public FilterOperation FilterOperation { get; set; }
        public object Value { get; set; }
        public string SecondValue { get; set; }
    }

    public enum FilterStatementConnector
    {
        And = 1,
        Or
    }

    public enum FilterOperation
    {
        EqualTo = 1,
        Contains,
        StartsWith,
        EndsWith,
        NotEqualTo,
        GreaterThan,
        GreaterThanOrEqualTo,
        LessThan,
        LessThanOrEqualTo,
        Between,
        IsNull,
        IsEmpty,
        IsNullOrWhiteSpace,
        IsNotNull,
        IsNotEmpty,
        IsNotNullNorWhiteSpace,
        In
    }

    //public enum TypeGroup
    //{
    //    [SupportedOperations(Operation.EqualTo, Operation.NotEqualTo)]
    //    Default,

    //    [SupportedOperations(Operation.Contains, Operation.EndsWith, Operation.EqualTo, Operation.IsEmpty, Operation.IsNotEmpty, Operation.IsNotNull, Operation.IsNotNullNorWhiteSpace, Operation.IsNull, Operation.IsNullOrWhiteSpace, Operation.NotEqualTo, Operation.StartsWith)]
    //    Text,

    //    [SupportedOperations(Operation.Between, Operation.EqualTo, Operation.GreaterThan, Operation.GreaterThanOrEqualTo, Operation.LessThan, Operation.LessThanOrEqualTo, Operation.NotEqualTo)]
    //    Number,

    //    [SupportedOperations(Operation.EqualTo, Operation.NotEqualTo)]
    //    Boolean,

    //    [SupportedOperations(Operation.Between, Operation.EqualTo, Operation.GreaterThan, Operation.GreaterThanOrEqualTo, Operation.LessThan, Operation.LessThanOrEqualTo, Operation.NotEqualTo)]
    //    Date,

    //    [SupportedOperations(Operation.IsNotNull, Operation.IsNull)]
    //    Nullable
    //}
}
