namespace TestProject1;

[TestClass]
public class UnitTest1
{
    //Allows writeline
    private TestContext testContextInstance;
    public TestContext TestContext
    {
        get { return testContextInstance; }
        set { testContextInstance = value; }
    }
    [TestMethod]
    public void AccountTest()
    {
        AccountModel model = new AccountModel(1, "@.@", "xyz", "Mark", false, false);
        Assert.IsTrue(model.Id==1);
    }
    
    [TestMethod]
    [DataRow(1, "Test1", "8D0123", "Test", false,false)]
    [DataRow(2, "Test2", "328W90", "Test1", true,false)]
    [DataRow(3, "Test3", "423P223", "Bigtest", false,false)]
    [DataRow(4, "Test4", "31A21321", "Test4", true,false)]
    public void TestAccount(int id, string email, string password, string name, bool employee, bool manager)
    {
       
        AccountModel account = new AccountModel(id, email, password, name, employee,manager);
        AccountsAccess.AddAccount(account.EmailAddress,account.Password,account.FullName,account.IsEmployee,account.IsManager);
        Assert.IsNotNull(AccountsAccess.LoadAll().Find(account => account.EmailAddress == email));
        AccountsAccess.ClearJsonFiles(1);
    }

    [TestMethod]
    public void TestReservations()
    {
        //Tried to add a datarow to this method, the datetime and timespan functions woudltn cooperate so i had to do this
        ReservationsLogic reservations = new ReservationsLogic();
        AccountsAccess.AddReservation(new ReservationModel("1S","Test@gmail.com",new DateTime(2003,12,2),4,new TimeSpan(12),new TimeSpan(13),"ABCDEFG"));
        var Reservation = AccountsAccess.LoadAllReservations().Find(account => account.EmailAddress == "Test@gmail.com");
        Assert.IsTrue(Reservation != null );
        AccountsAccess.ClearJsonFiles(2);
        
    }

    // test if you get dates back when calling populate dates()
    // and that the correct result is a 2D datetime array
    [TestMethod] 
    public void TestPopulateDates()
    {
        ReservationsLogic logic = new ReservationsLogic();
        var result = logic.PopulateDates();
        Assert.IsNotNull(result);
        Assert.IsTrue(result.GetType() == typeof(DateTime[,]));
    }
}