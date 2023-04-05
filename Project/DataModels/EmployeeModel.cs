
class EmployeeModel:AccountModel
{
    public EmployeeModel(int id, string emailAddress, string password, string fullName) : 
        base(id, emailAddress,password,fullName,true,false) { }
}