using AutoMapper;
using Rule.WebAPI.Model;
using Rule.WebAPI.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rule.WebAPI.Infrastructure.Mapping
{
    public class RuleMappingProfile : Profile
    {
        public RuleMappingProfile()
        {
            CreateMap<RuleEngineEntity, RuleEngine>()
                .ForMember(dest => dest.OperationId, src => src.MapFrom(x => (FilterOperation)x.FilterOperation))
                .ForMember(dest => dest.EntityTypeId, src => src.MapFrom(x => (EntityTypeEnum)x.EntityType))
                .ForMember(dest => dest.ConnectorId, src => src.MapFrom(x => (FilterStatementConnector)x.FilterConnector));

            CreateMap<RuleEngine, RuleEngineEntity>()
                .ForMember(dest => dest.FilterOperation, src => src.MapFrom(x => x.OperationId))
                .ForMember(dest => dest.EntityType, src => src.MapFrom(x => x.EntityTypeId))
                .ForMember(dest => dest.FilterConnector, src => src.MapFrom(x => x.ConnectorId));

            CreateMap<RuleEngine, RuleEngineResponseModel>()
             .ForMember(dest => dest.FilterConnector, src => src.MapFrom(x => x.Connector))
             .ForMember(dest => dest.EntityType, src => src.MapFrom(x => x.EntityType))
             .ForMember(dest => dest.FilterOperation, src => src.MapFrom(x => x.Operation));

            CreateMap<StatementConnector, FilterStatementConnectorResponse>();

            CreateMap<EntityType, EntityTypeResponse>()
                .AfterMap((s,d)=> {
                    var enumType = (EntityTypeEnum)Enum.ToObject(typeof(EntityTypeEnum), s.Id);
                    var properties = new List<string>();
                    switch (enumType)
                    {
                        case EntityTypeEnum.Aircraft:
                            properties = typeof(AircraftRequestModel).GetProperties().Select(f => f.Name).ToList();
                            break;
                        case EntityTypeEnum.Airport:
                            properties = typeof(AirportRequestModel).GetProperties().Select(f => f.Name).ToList();
                            break;
                        case EntityTypeEnum.Country:
                            properties = typeof(CountryRequestModel).GetProperties().Select(f => f.Name).ToList();
                            break;
                        case EntityTypeEnum.Person:
                            properties = typeof(PersonRequestModel).GetProperties().Select(f => f.Name).ToList();
                            break;
                        case EntityTypeEnum.Trips:
                            properties = typeof(TripRequestModel).GetProperties().Select(f => f.Name).ToList();
                            break;
                    }
                    d.Fields = properties;
                });

            CreateMap<NRule, NRuleResponse>()
                .ForMember(nr => nr.Rules, src => src.MapFrom(x => x.RuleEngines));

            CreateMap<Operation, FilterOperationResponse>();

            CreateMap<PersonRequestModel, Person>()
                .ForMember(dest => dest.Id, src => src.Ignore());

            CreateMap<Person, PersonRequestModel>();

        }
    }
}
