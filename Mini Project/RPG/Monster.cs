namespace RPG;

public class Monster
{
    int ID;
    string Name;
    string NamePlural;
    int MaximumDamage;
    int RewardExperience;
    int RewardGold;
    List Loot;
    int CurrentHitPoints;
    public Monster(int id, string name,
        string nameplural, int maximumDamage, int rewardExperience, int rewardGold, List loot, int currentHitPoints)
    {
        ID = id;
        Name = name;
        NamePlural = nameplural;
        MaximumDamage = maximumDamage;
        RewardExperience = rewardExperience;
        RewardGold = rewardGold;
        Loot = loot;
        CurrentHitPoints = currentHitPoints;
    }
}