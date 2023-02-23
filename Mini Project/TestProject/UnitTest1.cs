using RPG;

namespace TestProject;
[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void InventoryTest()
    {
        Player.CurrentLocation = World.LocationByID(World.LOCATION_ID_HOME);
        var rusty_sword = World.WeaponByID(World.WEAPON_ID_RUSTY_SWORD);
        var rusty_counted = (new CountedItem(new Item(rusty_sword.ID, rusty_sword.Name, rusty_sword.NamePlural), 1));
        Player player = new Player("John", 25, 25, 10, 0, 1, rusty_sword, Player.CurrentLocation);
        Assert.IsTrue(Player.CurrentLocation == World.LocationByID(World.LOCATION_ID_HOME));
        Player.Inventory.TheCountedItemList.Add(new CountedItem(new Item(rusty_sword.ID,rusty_sword.Name,rusty_sword.NamePlural),1));
        Player.Inventory.TheCountedItemList.Add(new CountedItem(new Item(5,"Apple","Apples"),5));
        Assert.IsTrue(Player.Inventory.TheCountedItemList.Contains(rusty_counted));
    }
}