using System.Text.Json.Serialization;


class ReservationModel
{
    [JsonPropertyName("table_id")]
    public int Id { get; set; }

    [JsonPropertyName("emailAddress")]
    public string EmailAddress { get; set; }

    [JsonPropertyName("res_date")]
    public DateTime Date { get; set; }
    
    public bool isReserved { get; set; }
    
    public ReservationModel(int id, string emailAddress, DateTime date, string fullName)
    {
        Id = id;
        EmailAddress = emailAddress;
        Date = date;
    }

}