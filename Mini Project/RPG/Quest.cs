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
    public CountedItemList QuestCompletionItems;
    public static int FARMER_COMPLETION_FLAG;
    public static int ALCHEMIST_COMPLETION_FLAG;
    public static int SPIDER_COMPLETION_FLAG;

    public Quest(int id, string name, string desc, int xp, int gold, Item item, Weapon weapon)
    {
        ID = id;
        Name = name;
        Description = desc;
        RewardXP = xp;
        RewardGold = gold;
        RewardItem = item;
        RewardWeapon = weapon;
        QuestCompletionItems = new CountedItemList();
        FARMER_COMPLETION_FLAG = 0;
        ALCHEMIST_COMPLETION_FLAG = 0;
        SPIDER_COMPLETION_FLAG = 0;
    }
    public static bool GameDone()
    {
        if (FARMER_COMPLETION_FLAG == 2 && ALCHEMIST_COMPLETION_FLAG == 2 && SPIDER_COMPLETION_FLAG == 2)
        {
            return true;
        }

        return false;
    }
}