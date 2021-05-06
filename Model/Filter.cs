using Rule.WebAPI.Infrastructure.Helper;
using Rule.WebAPI.Model.DTO;
using System.Collections.Generic;

namespace Rule.WebAPI.Model
{
    public class Filter<TClass> : IFilter where TClass : class
    {
        private List<IRuleEngineEntity> _statements;

        public IEnumerable<IRuleEngineEntity> Statements
        {
            get
            {
                return _statements.ToArray();
            }
        }

        public Filter()
        {
            _statements = new List<IRuleEngineEntity>();
        }

        public IFilterStatementConnection By<TPropertyType>(string propertyName, FilterOperation operation, TPropertyType value, TPropertyType value2 = default(TPropertyType), FilterStatementConnector connector = FilterStatementConnector.And)
        {
            return null;
            //var statement = new RuleEngineEntity(propertyName, operation, value, value2, connector);
            //_statements.Add(statement);
            //return new FilterStatementConnection<TClass>(this, statement);
        }
    }

    public interface IFilter
    {
        IEnumerable<IRuleEngineEntity> Statements { get; }
        IFilterStatementConnection By<TPropertyType>(string propertyName, FilterOperation operation, TPropertyType value, TPropertyType value2 = default(TPropertyType), FilterStatementConnector connector = FilterStatementConnector.And);
    }
}
