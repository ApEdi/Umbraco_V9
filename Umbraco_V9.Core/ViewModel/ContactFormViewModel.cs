using System.ComponentModel.DataAnnotations;

namespace Umbraco_V9.Core.ViewModel
{
    public class ContactFormViewModel
    {
        [Required]
        [MaxLength(80, ErrorMessage = "Please try and limit to 80 characters")]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid Email address")]
        public string EmailAddress { get; set; }

        [Required]
        [MaxLength(500, ErrorMessage = "Please try and limi your comments to 500 characters")]
        public string Comment { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "Please try and limi your comments to 255 characters")]
        public string Subject { get; set; }
    }
}
