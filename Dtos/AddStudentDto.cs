using HostelFinder.Models;
using System.ComponentModel.DataAnnotations;

namespace HostelFinder.Dtos
{
    public class AddStudentDto
    {
        public string MobileNumber { get; set; }

        public string StudentName { get; set; }

        
        public string GuardianName { get; set; }

        

        // Address Details

        public string State { get; set; }

        [Required]
        public string District { get; set; }

        
        public string Tehsil { get; set; }

        
        public string Pincode { get; set; }

        // Educational Information
        
        public string PreparationFor { get; set; } // e.g., JEE, NEET, Boards

       
        public string Class { get; set; } // e.g., 11, 12, Drop Year

        
        public string CoachingInstitute { get; set; } // e.g., Allen, PW, GCI

        
        public string Medium { get; set; } // e.g., Hindi, English

        // Hostel Info (optional until finalized)
        public string? HostelId { get; set; }

        

        public string? SeatPreferenceCode { get; set; } // e.g., ASA, ASN, NDA, etc.
    }
}

