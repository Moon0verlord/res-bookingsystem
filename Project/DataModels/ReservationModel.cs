using System.Text.Json.Serialization;


public class ReservationModel
{
    [JsonPropertyName("table_id")]
    public int Id { get; set; }

    [JsonPropertyName("emailAddress")]
    private string EmailAddress { get; set; }

    [JsonPropertyName("res_date")]
    public DateTime Date { get; set; }
    
    public bool isReserved { get; set; }
    
    //todo: add reservation id
    public ReservationModel(int id, string emailAddress, DateTime date)
    {
        Id = id;
        EmailAddress = emailAddress;
        Date = date;
    }

}