using System.ComponentModel.DataAnnotations;

namespace HostelFinder.Models
{
    public class Hostel
    {
        [Key]
        [Required]
        public string HostelId { get; set; } = string.Empty; // e.g., Hostel00001

        [Required]
        public string HostelName { get; set; }

        [Required]
        public string ManagerName { get; set; }

        [Required]
        [Phone]
        public string MobileNumber { get; set; }

        // Total Seat Distribution by preference codes
        public int ASA { get; set; }  // AC Single Attached
        public int ASN { get; set; }  // AC Single Non-Attached
        public int ADA { get; set; }
        public int ADN { get; set; }
        public int NSA { get; set; }
        public int NSN { get; set; }
        public int NDA { get; set; }
        public int NDN { get; set; }


        //price of rooms

        public int ASAPRICE { get; set; }  // AC Single Attached
        public int ASNPRICE { get; set; }  // AC Single Non-Attached
        public int ADAPRICE { get; set; }
        public int ADNPRICE { get; set; }
        public int NSAPRICE { get; set; }
        public int NSNPRICE { get; set; }
        public int NDAPRICE { get; set; }
        public int NDNPRICE { get; set; }

        // Available Seats by preference codes
        public int AvailableASA { get; set; }
        public int AvailableASN { get; set; }
        public int AvailableADA { get; set; }
        public int AvailableADN { get; set; }
        public int AvailableNSA { get; set; }
        public int AvailableNSN { get; set; }
        public int AvailableNDA { get; set; }
        public int AvailableNDN { get; set; }

        // Navigation: All students assigned to this hostel
        public ICollection<Student>? Students { get; set; }
    }
}

