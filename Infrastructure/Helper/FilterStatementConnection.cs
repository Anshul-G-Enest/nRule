using Rule.WebAPI.Model;
using Rule.WebAPI.Model.DTO;

namespace Rule.WebAPI.Infrastructure.Helper
{
    public class FilterStatementConnection<TClass> : IFilterStatementConnection where TClass : class
	{
		readonly IFilter _filter;
		readonly IRuleEngineEntity _statement;

		internal FilterStatementConnection(IFilter filter, IRuleEngineEntity statement)
		{
			_filter = filter;
			_statement = statement;
		}

		/// <summary>
		/// Defines that the last filter statement will connect to the next one using the 'AND' logical operator.
		/// </summary>
		public IFilter And
		{
			get
			{
				_statement.FilterConnector = FilterStatementConnector.And;
				return _filter;
			}
		}

		/// <summary>
		/// Defines that the last filter statement will connect to the next one using the 'OR' logical operator.
		/// </summary>
		public IFilter Or
		{
			get
			{
				_statement.FilterConnector = FilterStatementConnector.Or;
				return _filter;
			}
		}
	}

	public interface IFilterStatementConnection
    {
		IFilter And { get; }
		IFilter Or { get; }
	}
}
