namespace TestProject1;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
        AccountModel model = new AccountModel(1, "@.@", "xyz", "Mark", false, false);
        Assert.IsTrue(model.Id==1);
    }
}