namespace RPG;

public class Player
{
    string Name;
    int MaxHP;
    int CurrentHP;
    int gold;
    int XP;
    int level;
    Weapon CurrentWeapon;
    Location CurrentLocation;
    
    public Player(string Name, int MaxHP, int CurrentHP, int gold,
        int XP, int level, Weapon CurrentWeapon, Location CurrentLocation)
    {
        this.Name = Name;
        MaxHP = 100;
        this.CurrentHP = CurrentHP;
        this.gold = gold;
        this.XP = XP;
        this.level = level;
        this.CurrentWeapon = CurrentWeapon;
        this.CurrentLocation = CurrentLocation;
    }
}