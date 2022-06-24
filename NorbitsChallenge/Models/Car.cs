using System.ComponentModel.DataAnnotations;

namespace NorbitsChallenge.Models
{
    public class Car
    {
        [Display(Name = "License plate")]
        [Required(ErrorMessage ="Car license plate is required")]
        [StringLength(7,MinimumLength =7, ErrorMessage = "Licenseplate must be 7 characters long")]
        public string LicensePlate  { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Display(Name = "Model")]
        [Required(ErrorMessage = "Model is required")]
        public string Model { get; set; }

        [Display(Name = "Brand")]
        [Required(ErrorMessage = "Brand is required")]
        public string Brand { get; set; }

        [Display(Name = "TireCount")]
        [Range(0,16)]
        [Required(ErrorMessage = "Tirecount must be between 0 and 16")]
        public int TireCount { get; set; }

        [Display(Name = "CompanyID")]
        [Range(0, 50)]
        [Required(ErrorMessage = "CompanyID must be between 0 and 50")]
        public int CompanyID { get; set; }

    }
}


