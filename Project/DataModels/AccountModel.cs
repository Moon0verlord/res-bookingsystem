using System.Text.Json.Serialization;


public class AccountModel:IComparable<AccountModel>
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
    public bool LoggedIn { get; set; }
    public AccountModel(int id, string emailAddress, string password, string fullName,bool isemployee,bool ismanager)
    {
        Id = id;
        EmailAddress = emailAddress;
        Password = BCrypt.Net.BCrypt.HashPassword(password, 12);
        FullName = fullName;
        IsEmployee = isemployee;
        IsManager = ismanager;
    }
    // can only be placed here, is used in EmployeeManagerLogic
    public int CompareTo(AccountModel? other)
    {
        if (other == null) { return 1;}
        
        int res = EmailAddress.CompareTo(other.EmailAddress);
        if (res == 0) 
        {
            res = Id.CompareTo(other.Id);
        }
        return res;
    }

}




