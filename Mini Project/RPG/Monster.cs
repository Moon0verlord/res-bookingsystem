namespace RPG;

public class Monster
{
    public int ID;
    public static string Name;
    string NamePlural;
    int MaximumDamage;
    int RewardExperience;
    int RewardGold;
    public CountedItemList Loot;
    int CurrentHitPoints;
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