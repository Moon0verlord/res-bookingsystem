using System.Text.Json.Serialization;


class AccountModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("emailAddress")]
    public string EmailAddress { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }

    [JsonPropertyName("fullName")]
    public string FullName { get; set; }
    
    [JsonPropertyName("Employee")]
    public bool IsEmployee { get; set; }
    
    [JsonPropertyName("Manager")]
    public bool IsManager { get; set; }
    
    [JsonIgnore]
    public bool loggedIn { get; set; }
    public AccountModel(int id, string emailAddress, string password, string fullName,bool isemployee,bool ismanager)
    {
        Id = id;
        EmailAddress = emailAddress;
        Password = password;
        FullName = fullName;
        IsEmployee = isemployee;
        IsManager = ismanager;
    }

}




