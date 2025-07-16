namespace ic_ASPnet.Models
{
    public class AttendanceLog
    {
    public DateTime TimeStamp { get; set; }
    public required string Name { get; set; }
    public required string CardUid { get; set; }
    public required string Type { get; set; } // "IN" or "OUT"

    }
}
