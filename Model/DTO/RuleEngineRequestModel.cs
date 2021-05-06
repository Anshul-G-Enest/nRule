using Rule.WebAPI.Infrastructure;
using System.Collections.Generic;
using System.Dynamic;

namespace Rule.WebAPI.Model.DTO
{
    public class RuleEngineRequestModel
    {
        public string JsonData { get; set; }
        public int RuleId { get; set; }
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
        public object SecondValue { get; set; }
        public EntityTypeEnum EntityType { get; set; }
        public dynamic EntityName { get; set; } = new ExpandoObject();

        //public RuleEngineEntity(string propertyId, FilterOperation operation, object value, object value2 = default(object), FilterStatementConnector connector = FilterStatementConnector.And)
        //{
        //    PropertyName = propertyId;
        //    FilterConnector = connector;
        //    FilterOperation = operation;
        //    if (typeof(object).IsArray)
        //    {
        //        if (operation != FilterOperation.Contains && operation != FilterOperation.In) 
        //            throw new ArgumentException("Only 'Operation.Contains' and 'Operation.In' support arrays as parameters.");

        //        var listType = typeof(List<>);
        //        var constructedListType = listType.MakeGenericType(typeof(object).GetElementType());
        //        Value = value != null ? Activator.CreateInstance(constructedListType, value) : null;
        //        SecondValue = value2 != null ? Activator.CreateInstance(constructedListType, value2) : null;
        //    }
        //    else
        //    {
        //        Value = value;
        //        SecondValue = value2;
        //    }

        //    var helper = new OperationHelper();
        //    ValidateSupportedOperations(helper);
        //}
        //private void ValidateSupportedOperations(OperationHelper helper)
        //{
        //    List<FilterOperation> supportedOperations = null;
        //    if (typeof(object) == typeof(object))
        //    {
        //        //TODO: Issue regarding the TPropertyType that comes from the UI always as 'Object'
        //        //supportedOperations = helper.GetSupportedOperations(Value.GetType());
        //        System.Diagnostics.Debug.WriteLine("WARN: Not able to check if the operation is supported or not.");
        //        return;
        //    }

        //    supportedOperations = helper.SupportedOperations(typeof(object));

        //    if (!supportedOperations.Contains(FilterOperation))
        //    {
        //        throw new Exception();
        //    }
        //}

    }

    public interface IRuleEngineEntity
    {
        int Id { get; set; }
        FilterStatementConnector FilterConnector { get; set; }
        string PropertyName { get; set; }
        FilterOperation FilterOperation { get; set; }
        object Value { get; set; }
        object SecondValue { get; set; }
        EntityTypeEnum EntityType { get; set; }
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

    public enum TypeGroup
    {
        [SupportedOperations(FilterOperation.EqualTo, FilterOperation.NotEqualTo)]
        Default,

        [SupportedOperations(FilterOperation.Contains, FilterOperation.EndsWith, FilterOperation.EqualTo, FilterOperation.IsEmpty, FilterOperation.IsNotEmpty, FilterOperation.IsNotNull, FilterOperation.IsNotNullNorWhiteSpace, FilterOperation.IsNull, FilterOperation.IsNullOrWhiteSpace, FilterOperation.NotEqualTo, FilterOperation.StartsWith)]
        Text,

        [SupportedOperations(FilterOperation.Between, FilterOperation.EqualTo, FilterOperation.GreaterThan, FilterOperation.GreaterThanOrEqualTo, FilterOperation.LessThan, FilterOperation.LessThanOrEqualTo, FilterOperation.NotEqualTo)]
        Number,

        [SupportedOperations(FilterOperation.EqualTo, FilterOperation.NotEqualTo)]
        Boolean,

        [SupportedOperations(FilterOperation.Between, FilterOperation.EqualTo, FilterOperation.GreaterThan, FilterOperation.GreaterThanOrEqualTo, FilterOperation.LessThan, FilterOperation.LessThanOrEqualTo, FilterOperation.NotEqualTo)]
        Date,

        [SupportedOperations(FilterOperation.IsNotNull, FilterOperation.IsNull)]
        Nullable
    }

    public enum EntityTypeEnum
    {
        Airport = 1,
        Country,
        Aircraft,
        Trips,
        Person
    }
}
