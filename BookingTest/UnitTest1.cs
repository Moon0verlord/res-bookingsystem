using System.Text.RegularExpressions;
using System.ComponentModel;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;
namespace TestProject1;

[TestClass]
public class UnitTest1
{
    
    //Allows write line
    private TestContext testContextInstance;

    public TestContext TestContext
    {
        get => testContextInstance;
        set => testContextInstance = value; 
    }
    //Made by martijn 
    //Test if multiple accounts can be added correctly
    [TestMethod]
    [DataRow(1, "Test1", "8D0123", "Test", false, false)]
    [DataRow(2, "Test2", "328W90", "Test1", true, false)]
    [DataRow(3, "Test3", "423P223", "Bigtest", false, false)]
    [DataRow(4, "Test4", "31A21321", "Test4", true, false)]
    public void TestAccount(int id, string email, string password, string name, bool employee, bool manager)
    {

        AccountModel account = new AccountModel(id, email, password, name, employee, manager);
        AccountsAccess.AddAccount(account.EmailAddress, account.Password, account.FullName, account.IsEmployee, account.IsManager);
        Assert.IsNotNull(AccountsAccess.LoadAll().Find(userAcc => userAcc.EmailAddress == email));
        AccountsAccess.ClearJsonFiles(1);
    }
    //Made my Martijn
    //Tests if reservations can be added correctly 
    [TestMethod]
    public void TestReservations()
    {
        //Tried to add a data row to this method, the datetime and timespan functions woudltn cooperate so i had to do this
        AccountsAccess.AddReservation(new ReservationModel("1S", "Test@gmail.com", new DateTime(2003, 12, 2), 4, new TimeSpan(12), new TimeSpan(13), "RES-180439", 2));
        var reservation = AccountsAccess.LoadAllReservations().Find(account => account.EmailAddress == "Test@gmail.com");
        Assert.IsTrue(reservation != null);
        AccountsAccess.ClearJsonFiles(2);

    }
    
    // made by robin b
    // create id used by reservations, and check if it matches the following regular expression.
    [TestMethod]
    public void TestidCreation()
    {
        ReservationsLogic logic = new ReservationsLogic();
        Regex regex = new Regex(@"RES-\d+");
        string id = logic.CreateID();
        Assert.IsTrue(regex.IsMatch(id));
    }

    // made by robin b
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

    // made by Jona
    // tests if the email is valid
    [TestMethod]
    public void TestIsValidEmail()
    {
        string email = " ";
        Assert.IsFalse(EmailLogic.IsValidEmail(email));
        email = "test";
        Assert.IsFalse(EmailLogic.IsValidEmail(email));
        email = "test@";
        Assert.IsFalse(EmailLogic.IsValidEmail(email));
        email = "test@test";
        Assert.IsFalse(EmailLogic.IsValidEmail(email));
        email = "test@test.";
        Assert.IsFalse(EmailLogic.IsValidEmail(email));
        email = "test@gmail.com";
        Assert.IsTrue(EmailLogic.IsValidEmail(email));
    }


    // Made by Rafiq

    [TestMethod]
    public void HidePass_ReturnsHiddenPassword()
    {
        string password = "mysecretpassword";
        string expectedHiddenPassword = "****************";
        string actualHiddenPassword = UserLogin.HidePass(password);
        Assert.AreEqual(expectedHiddenPassword, actualHiddenPassword);
    }

    // Made by Robin O
    // test if the input is correct

    [TestMethod]
    public void TestCheckInput()
    {
        string answer = "j";
        int expectAns = 1;
        int actAns = AnswerLogic.CheckInput(answer);
        Assert.AreEqual(expectAns, actAns);
    }
    [TestMethod]

    // Made by Martijn & Jona
    // test if the email is sent
    public void TestingMail()
    {
        EmailLogic.SendEmail("j@j.com",new DateTime(2003,12,3),"RE1",new TimeSpan(10,2,3),
            new TimeSpan(12,0,0));
        Assert.IsTrue(EmailLogic.mailSent);
    }
}
