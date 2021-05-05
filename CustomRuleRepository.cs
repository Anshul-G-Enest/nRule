using NRules.RuleModel;
using NRules.RuleModel.Builders;
using Rule.WebAPI.Model;
using Rule.WebAPI.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Rule.WebAPI
{
    public class CustomRuleRepository : IRuleRepository
    {
        private readonly IRuleSet _ruleSet;
        readonly MethodInfo stringContainsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
        readonly MethodInfo stringStartsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
        readonly MethodInfo stringEndsWithMethod = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });

        public CustomRuleRepository()
        {
            _ruleSet = new RuleSet("default");
        }
        public IEnumerable<IRuleSet> GetRuleSets()
        {
            //Merge
            var sets = new List<IRuleSet>();
            sets.Add(_ruleSet);
            return sets;
        }

        public void LoadRules<T>(RuleEngineRequestModel list)
        {
            _ruleSet.Add(
                BuildRule<T>(list)
            );
        }

        private List<IRuleDefinition> BuildRule<T>(RuleEngineRequestModel ruleEngineRequestModel)
        {
            var builder = new RuleBuilder();
            builder.Name(ruleEngineRequestModel.RuleName);
            var orGroup = builder.LeftHandSide().Group(GroupType.Or);
            Expression expression = null;
            var andGroup = orGroup.Group(GroupType.And);
            PatternBuilder modelPattern = andGroup.Pattern(typeof(T), nameof(Person));
            var modelParameter = modelPattern.Declaration.ToParameterExpression();

            var member = Expression.Property(modelParameter, nameof(PersonRuleRequestModel.Person));
            foreach (var item in ruleEngineRequestModel.Rules)
            {
                Expression condition = null;
                switch (item.FilterOperation)
                {
                    case FilterOperation.GreaterThan:
                        condition = Expression.GreaterThan(Expression.Property(member, item.PropertyName), Expression.Constant(Convert.ToInt32(item.Value.ToString())));
                        break;
                    case FilterOperation.GreaterThanOrEqualTo:
                        condition = Expression.GreaterThanOrEqual(Expression.Property(member, item.PropertyName), Expression.Constant(Convert.ToInt32(item.Value.ToString())));
                        break;
                    case FilterOperation.EqualTo:
                        if(Int32.TryParse(item.Value.ToString(), out int number))
                            condition = Expression.Equal(Expression.Property(member, item.PropertyName), Expression.Constant(Convert.ToInt32(item.Value.ToString())));
                        else if (Boolean.TryParse(item.Value.ToString(), out bool male))
                            condition = Expression.Equal(Expression.Property(member, item.PropertyName), Expression.Constant(Convert.ToBoolean(item.Value.ToString())));
                        else
                            condition = Expression.Equal(Expression.Property(member, item.PropertyName), Expression.Constant(item.Value.ToString().ToLower()));
                        break;
                    case FilterOperation.NotEqualTo:
                        if (Int32.TryParse(item.Value.ToString(), out int num))
                            condition = Expression.NotEqual(Expression.Property(member, item.PropertyName), Expression.Constant(Convert.ToInt32(item.Value.ToString())));
                        else if (Boolean.TryParse(item.Value.ToString(), out bool male))
                            condition = Expression.NotEqual(Expression.Property(member, item.PropertyName), Expression.Constant(Convert.ToBoolean(item.Value.ToString())));
                        else
                            condition = Expression.NotEqual(Expression.Property(member, item.PropertyName), Expression.Constant(item.Value.ToString().ToLower()));
                        break;
                    case FilterOperation.LessThan:
                        condition = Expression.LessThan(Expression.Property(member, item.PropertyName), Expression.Constant(Convert.ToInt32(item.Value.ToString())));
                        break;
                    case FilterOperation.LessThanOrEqualTo:
                        condition = Expression.LessThanOrEqual(Expression.Property(member, item.PropertyName), Expression.Constant(Convert.ToInt32(item.Value.ToString())));
                        break;
                    case FilterOperation.IsNull:
                    case FilterOperation.IsNotNull:
                        condition = Expression.Equal(Expression.Property(member, item.PropertyName), Expression.Constant(null));
                        break;
                    case FilterOperation.Contains:
                        var constantContainExpr = Expression.Constant(item.Value.ToString().ToLower());
                        condition = Expression.Call(Expression.Property(member, item.PropertyName), stringContainsMethod, constantContainExpr);
                        break;
                    case FilterOperation.StartsWith:
                        var constantStartExpr = Expression.Constant(item.Value.ToString().ToLower());
                        condition = Expression.Call(Expression.Property(member, item.PropertyName), stringStartsWithMethod, constantStartExpr);
                        break;
                    case FilterOperation.EndsWith:
                        var constantEndExpr = Expression.Constant(item.Value.ToString().ToLower());
                        condition = Expression.Call(Expression.Property(member, item.PropertyName), stringEndsWithMethod, constantEndExpr);
                        break;
                    case FilterOperation.Between:
                        var constant = Expression.Constant(item.Value.ToString().ToLower());
                        var constant2 = Expression.Constant(item.SecondValue.ToString().ToLower());
                        BinaryExpression leftBinaryExpression = null;
                        BinaryExpression rightBinaryExpression = null;
                        if (Int32.TryParse(item.Value.ToString(), out int idField))
                        {
                            leftBinaryExpression = Expression.GreaterThanOrEqual(Expression.Property(member, item.PropertyName), Expression.Constant(Convert.ToInt32(item.Value.ToString())));
                            rightBinaryExpression = Expression.LessThanOrEqual(Expression.Property(member, item.PropertyName), Expression.Constant(Convert.ToInt32(item.SecondValue.ToString())));
                        }
                        if(DateTime.TryParse(item.Value.ToString(),out DateTime dateTime))
                        {
                            leftBinaryExpression = Expression.GreaterThanOrEqual(Expression.Property(member, item.PropertyName), Expression.Constant(Convert.ToDateTime(item.Value.ToString())));
                            rightBinaryExpression = Expression.LessThanOrEqual(Expression.Property(member, item.PropertyName), Expression.Constant(Convert.ToDateTime(item.SecondValue.ToString())));
                        }
                        condition = CombineExpressions(leftBinaryExpression, rightBinaryExpression, FilterStatementConnector.And);
                        break;

                }
                expression = expression == null ? condition : CombineExpressions(expression, condition, item.FilterConnector);
            }
            var finalexpression = Expression.Lambda(expression, modelParameter);
            modelPattern.Condition(finalexpression);

            Expression<Action<IContext>> action = ctx => Console.WriteLine("Action triggered");

            builder.RightHandSide().Action(action);

            var rule = builder.Build();
            return new List<IRuleDefinition> { rule };
        }

        private Expression CombineExpressions(Expression expr1, Expression expr2, FilterStatementConnector connector)
        {
            return connector == FilterStatementConnector.And ? Expression.AndAlso(expr1, expr2) : Expression.OrElse(expr1, expr2);
        }
    }
}
