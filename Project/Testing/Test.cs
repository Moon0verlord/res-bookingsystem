using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;
namespace Testing;

[TestClass]
public class UnitTest1
{
    private TestContext testContextInstance;

    /// <summary>
    /// Allows a writeline in the form of TextContext.Writeline
    /// </summary>
    public TestContext TestContext
    {
        get { return testContextInstance; }
        set { testContextInstance = value; }
    }
    [TestMethod]
    public void TestReservation()
    {
        TestContext.WriteLine(Environment.CurrentDirectory);
        Assert.IsTrue(1==1);
    }
}