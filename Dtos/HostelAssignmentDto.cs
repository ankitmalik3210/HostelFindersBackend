namespace HostelFinder.Dtos
{
    public class HostelAssignmentDto
    {
        public string HostelId { get; set; }  // Nullable if vacated
        public string SeatPreferenceCode { get; set; }  // Nullable if vacated
        public string Money { get; set; }
    }
}
