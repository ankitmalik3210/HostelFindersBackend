using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HostelFinder.Models
{
    public class Student
    {
        [Key]
        [Required]
        public string MobileNumber { get; set; }  = string.Empty;

        [Required]
        public string StudentName { get; set; }

        [Required]
        public string GuardianName { get; set; }

        [Required]
        
        // Address Details
       
        public string State { get; set; }

        [Required]
        public string District { get; set; }

        [Required]
        public string Tehsil { get; set; }

        [Required]
        public string Pincode { get; set; }

        // Educational Information
        [Required]
        public string PreparationFor { get; set; } // e.g., JEE, NEET, Boards

        [Required]
        public string Class { get; set; } // e.g., 11, 12, Drop Year

        [Required]
        public string CoachingInstitute { get; set; } // e.g., Allen, PW, GCI

        [Required]
        public string Medium { get; set; } // e.g., Hindi, English

        // Hostel Info (optional until finalized)
        public string? HostelId { get; set; }

        public string? Money { get; set; }

        public Hostel? Hostel { get; set; }

        public string? SeatPreferenceCode { get; set; } // e.g., ASA, ASN, NDA, etc.
    }
}
