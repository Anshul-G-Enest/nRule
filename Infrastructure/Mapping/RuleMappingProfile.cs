using AutoMapper;
using Rule.WebAPI.Model;
using Rule.WebAPI.Model.DTO;

namespace Rule.WebAPI.Infrastructure.Mapping
{
    public class RuleMappingProfile : Profile
    {
        public RuleMappingProfile()
        {
            CreateMap<RuleEngineEntity, RuleEngine>()
                .ForMember(dest => dest.OperationId, src => src.MapFrom(x => (FilterOperation)x.FilterOperation))
                .ForMember(dest => dest.ConnectorId, src => src.MapFrom(x => (FilterStatementConnector)x.FilterConnector));

            CreateMap<RuleEngine, RuleEngineEntity>()
                .ForMember(dest => dest.FilterOperation, src => src.MapFrom(x => x.OperationId))
                .ForMember(dest => dest.FilterConnector, src => src.MapFrom(x => x.ConnectorId));

            CreateMap<RuleEngine, RuleEngineResponseModel>()
             .ForMember(dest => dest.FilterConnector, src => src.MapFrom(x => x.Connector))
             .ForMember(dest => dest.FilterOperation, src => src.MapFrom(x => x.Operation));

            CreateMap<StatementConnector, FilterStatementConnectorResponse>();

            CreateMap<NRule, NRuleResponse>()
                .ForMember(nr => nr.Rules, src => src.MapFrom(x => x.RuleEngines));


            CreateMap<Operation, FilterOperationResponse>();

            CreateMap<PersonRequestModel, Person>()
                .ForMember(dest => dest.Id, src => src.Ignore());

            CreateMap<Person, PersonRequestModel>();



        }
    }
}
