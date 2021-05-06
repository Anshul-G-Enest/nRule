using System;
using System.Collections.Generic;

namespace Rule.WebAPI.Model.DTO
{
    public class ExecuteRuleRequestModel
    {
        //public ExecuteRuleRequestModel(IRuleRequest ruleRequest)
        //{
        //    MyRule = ruleRequest;
        //}

        public IRuleRequest MyRule { get; set; }


        public PersonRequestModel Person { get; set; }
        public AirportRequestModel Airport { get; set; }
        public CountryRequestModel Country { get; set; }
        public TripRequestModel Trip { get; set; }
        public AircraftRequestModel Aircraft { get; set; }
    }

    public interface IRuleRequest { }

    public class PersonRequestModel : IRuleRequest
    {
        public int Id { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public bool IsMale { get; set; }
    }

    public class AirportRequestModel : IRuleRequest
    {
        public bool AirportofEntry { get; set; }
        public bool MilitaryAirport { get; set; }
    }

    public class CountryRequestModel : IRuleRequest
    {
        public List<string> EuCountry { get; set; }
    }

    public class TripRequestModel: IRuleRequest
    {
        public List<string> ArrivalAirport { get; set; }
        public List<string> ArrivalState { get; set; }
        public bool PaxDisembarkation { get; set; }
        public string Route { get; set; }
        public int MachSpeed { get; set; }
    }

    public class AircraftRequestModel: IRuleRequest
    {
        public List<string> ICAOAerodromeReferenceCode { get; set; }
        public List<string> AirworthinessCertificateType { get; set; }
        public List<string> NoiseStage { get; set; }
        public List<string> RegistrationNationality { get; set; }
        public List<string> RegistrationNationalitySovereignty { get; set; }
        public List<string> AircraftCategory { get; set; }
        public int MTOW { get; set; }
        public int PassengerSeatCapacity { get; set; }
    }
}
