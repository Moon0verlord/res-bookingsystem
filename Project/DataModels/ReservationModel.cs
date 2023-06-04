using System.Text.Json.Serialization;


public class ReservationModel
{
    [JsonPropertyName("table_id")]
    public string Id { get; set; }
    
    [JsonPropertyName("res_id")]
    
    public string ResId { get; set; }

    [JsonPropertyName("emailAddress")]
    public string? EmailAddress { get; set; }

    [JsonPropertyName("res_date")]
    public DateTime Date { get; set; }
    
    [JsonPropertyName("group_size")]
    public int GroupSize { get; set; }
    
    [JsonPropertyName("start_time")]
    public TimeSpan StartTime { get; set; }
    
    [JsonPropertyName("leave_time")]
    public TimeSpan LeaveTime { get; set; }
    
    [JsonPropertyName("chosen_course")]
    public int Course { get; set; }
    
    [JsonIgnore]
    public bool IsReserved { get; set; }
    
    [JsonIgnore]
    public int TableSize { get; set; }
    
    
    public ReservationModel(string id, string? emailAddress, DateTime date, int groupsize, TimeSpan starttime, TimeSpan leavetime, string res_id, int course)
    {
        Id = id;
        EmailAddress = emailAddress;
        Date = date;
        GroupSize = groupsize;
        StartTime = starttime;
        LeaveTime = leavetime;
        ResId = res_id;
        Course = course;
        IsReserved = false;
    }
}