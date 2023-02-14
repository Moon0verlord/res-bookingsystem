namespace RPG;

public class Quest
{
    public int ID;
    public string Name;
    public string Description;
    public int RewardXP;
    public int RewardGold;
    public Item RewardItem;
    public Weapon RewardWeapon;

    public Quest(int id, string name, string desc, int xp, int gold, Item item, Weapon weapon)
    {
        ID = id;
        Name = name;
        Description = desc;
        RewardXP = xp;
        RewardGold = gold;
        RewardItem = item;
        RewardWeapon = weapon;
    }
}