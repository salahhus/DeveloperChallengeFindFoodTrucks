using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FindFoodTrucksWebApp.Models
{
    /// <summary>
    /// Input model wich holds user's inputs for filtering
    /// </summary>
    public class InputModel
    {
        [Required]
        public string Latitude { get; set; } = string.Empty;

        [Required]
        public string Longitude { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Only positive numbers allowed.")]
        public int AmountOfResults { get; set; } = 0;

        [DisplayName("Preferred food")]
        public string PreferredFood { get; set; } = string.Empty;
    }
}
