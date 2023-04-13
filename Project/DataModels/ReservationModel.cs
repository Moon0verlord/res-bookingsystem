using System.Text.Json.Serialization;


public class ReservationModel
{
    [JsonPropertyName("table_id")]
    public int Id { get; set; }

    [JsonPropertyName("emailAddress")]
    public string EmailAddress { get; set; }

    [JsonPropertyName("res_date")]
    public DateTime Date { get; set; }
    
    [JsonPropertyName("group_size")]
    public int GroupSize { get; set; }
    
    [JsonPropertyName("start_time")]
    public TimeSpan StartTime { get; set; }
    
    [JsonPropertyName("leave_time")]
    public TimeSpan LeaveTime { get; set; }
    
    [JsonIgnore]
    public bool isReserved { get; set; }
    
    //todo: add reservation id
    public ReservationModel(int id, string emailAddress, DateTime date, int groupsize, TimeSpan starttime, TimeSpan leavetime)
    {
        Id = id;
        EmailAddress = emailAddress;
        Date = date;
        GroupSize = groupsize;
        StartTime = starttime;
        LeaveTime = leavetime;
        isReserved = false;
    }
}