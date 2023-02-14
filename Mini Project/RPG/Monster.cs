namespace RPG;

public class Monster
{
    public int ID;
    string Name;
    string NamePlural;
    int MaximumDamage;
    int RewardExperience;
    int RewardGold;
    public List<int> Loot;
    int CurrentHitPoints;
    public Monster(int id, string name,
        string nameplural, int maximumDamage, int rewardExperience, int rewardGold, int loot, int currentHitPoints)
    {
        ID = id;
        Name = name;
        NamePlural = nameplural;
        MaximumDamage = maximumDamage;
        RewardExperience = rewardExperience;
        RewardGold = rewardGold;
        CurrentHitPoints = currentHitPoints;
    }
    
}