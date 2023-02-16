namespace RPG;

public class Monster
{
    public int ID;
    public string Name;
    public string NamePlural;
    public static int MaximumDamage;
    public int RewardExperience;
    public int RewardGold;
    public CountedItemList Loot;
    public int CurrentHitPoints;
    public Monster(int id, string name,
        string nameplural, int maximumDamage, int rewardExperience, int rewardGold,
        int loot, int currentHitPoints)
    {
        
        ID = id;
        Name = name;
        NamePlural = nameplural;
        MaximumDamage = maximumDamage;
        RewardExperience = rewardExperience;
        RewardGold = rewardGold;
        CurrentHitPoints = currentHitPoints;
        Loot = new CountedItemList();
    }
    
}