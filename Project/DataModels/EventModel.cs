using System.Text.Json.Serialization;


public class EventModel
{
    [JsonPropertyName("eventname")]
    public string EventName { get; set; }

    [JsonPropertyName("eventinfo")]
    public string EventInfo { get; set; }

    [JsonPropertyName("eventdate")]
    public string EventDate { get; set; }

    public EventModel(string eventname, string eventinfo, string eventdate)
    {
        EventName = eventname;
        EventInfo = eventinfo;
        EventDate = eventdate;
    }
}




