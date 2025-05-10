namespace HostelFinder.Dtos
{
    public class AddHostelDto
    {
        public string HostelId { get; set; }
        public string HostelName { get; set; }
        public string ManagerName { get; set; }
        public string MobileNumber { get; set; }

        // Total seats (Admin will only provide these)
        public int ASA { get; set; }
        public int ASN { get; set; }
        public int ADA { get; set; }
        public int ADN { get; set; }
        public int NSA { get; set; }
        public int NSN { get; set; }
        public int NDA { get; set; }
        public int NDN { get; set; }

        public int ASAPRICE { get; set; }
        public int ASNPRICE { get; set; }
        public int ADAPRICE { get; set; }
        public int ADNPRICE { get; set; }
        public int NSAPRICE { get; set; }
        public int NSNPRICE { get; set; }
        public int NDAPRICE { get; set; }
        public int NDNPRICE { get; set; }
    }
}
