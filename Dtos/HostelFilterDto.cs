namespace HostelFinder.Dtos
{
    public class HostelFilterDto
    {
            public int NumberOfStudents { get; set; }
            public string HostelId { get; set; }
            public string HostelName { get; set; }
            public string ManagerName { get; set; }
            public string ManagerMobile { get; set; }
            public Dictionary<string, RoomInfoDto> AvailableRooms { get; set; }
        
    }
}
