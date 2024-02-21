using CsvHelper.Configuration.Attributes;
using FindFoodTrucksWebApp.Enums;
using System.ComponentModel;
using System.Security.Cryptography;

namespace FindFoodTrucksWebApp.Models
{
    public class MobileFoodFacilityPermitModel
    {
        [Index(0)]
        public string Locationid { get; set; } = string.Empty;

        [Index(1)]
        [DisplayName("Company")]
        public string ApplicantName { get; set;} = string.Empty;

        [Index(2)]
        [DisplayName("Facility type")]
        public string FacilityType { get; set; } = string.Empty;

        [Index(4)]
        [DisplayName("Location")]
        public string LocationDescription { get; set; } = string.Empty;

        [Index(5)]
        [DisplayName("Address")]
        public string Address { get; set; } = string.Empty;

        [Index(6)]
        public string Blocklot { get; set; } = string.Empty;

        [Index(7)]
        public string Block { get; set; } = string.Empty;

        [Index(8)]
        public string Lot { get; set; } = string.Empty;

        [Index(9)]
        public string Permit { get; set; } = string.Empty;

        [Index(10)]
        [DisplayName("Status")]
        public string Status { get; set; } = string.Empty;

        [Index(11)]
        [DisplayName("Food items")]
        public string FoodItems { get; set; } = string.Empty;

        [Index(12)]
        public string X { get; set; } = string.Empty;

        [Index(13)]
        public string Y { get; set; } = string.Empty;

        [Index(14)]
        [DisplayName("Latitude")]
        public string Latitude { get; set; } = string.Empty;

        [Index(15)]
        [DisplayName("Longitude")]
        public string Longitude { get; set; } = string.Empty;

        [Index(16)]
        public string Schedule { get; set; } = string.Empty;

        [Index(17)]
        [DisplayName("Open hours")]
        public string DaysHours { get; set; } = string.Empty;

        [Index(23)]
        public string Location { get; set; } = string.Empty;
    }
}
