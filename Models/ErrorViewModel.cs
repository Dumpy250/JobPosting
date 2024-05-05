using System.ComponentModel.DataAnnotations;

namespace JobPosting.Models
{
    public class ErrorViewModel
    {
        [Required]
        public string RequestId { get; set; }

        [Required]
        public string ErrorMessage { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}