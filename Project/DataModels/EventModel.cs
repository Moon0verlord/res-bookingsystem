using System.Text.Json.Serialization;


public class EventModel
{
    [JsonPropertyName("eventname")]
    public string EventName { get; set; }

    [JsonPropertyName("eventinfo")]
    public string EventInfo { get; set; }

    [JsonPropertyName("eventdate")]
    public string EventDate { get; set; }

    public EventModel(string eventName, string eventInfo, string eventDate)
    {
        EventName = eventName;
        EventInfo = eventInfo;
        EventDate = eventDate;
    }
}




