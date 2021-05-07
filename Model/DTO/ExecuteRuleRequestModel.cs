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

        //public IRuleRequest MyRule { get; set; }

        public PersonRequestModel Person { get; set; }
        public AirportRequestModel Airport { get; set; }
        public CountryRequestModel Country { get; set; }
        public TripRequestModel Trip { get; set; }
        public AircraftRequestModel Aircraft { get; set; }
    }

    public class PersonRequestModel 
    {
        public int Id { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value.ToLower(); }
        }
        public bool IsMale { get; set; }
    }

    public class AirportRequestModel 
    {
        public bool AirportofEntry { get; set; }
        public bool MilitaryAirport { get; set; }
    }

    public class CountryRequestModel 
    {
        public int Id { get; set; }
        public List<string> EuCountry { get; set; }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value.ToLower(); }
        }
    }

    public class TripRequestModel
    {
        public List<string> ArrivalAirport { get; set; }
        public List<string> ArrivalState { get; set; }
        public bool PaxDisembarkation { get; set; }
        public string Route { get; set; }
        public int MachSpeed { get; set; }
    }

    public class AircraftRequestModel
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
