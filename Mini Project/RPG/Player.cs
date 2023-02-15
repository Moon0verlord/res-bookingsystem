namespace RPG;

public class Player
{
    public string Name;
    public int MaxHP;
    public int CurrentHP;
    public int gold;
    public int XP;
    public int level;
    public static Weapon CurrentWeapon;
    public static Location CurrentLocation;
    
    public Player(string Name, int MaxHP, int CurrentHP, int gold,
        int XP, int level, Weapon currentWeapon, Location currentLocation)
    {
        this.Name = Name;
        MaxHP = 100;
        this.CurrentHP = CurrentHP;
        this.gold = gold;
        this.XP = XP;
        this.level = level;
        CurrentWeapon = currentWeapon;
        CurrentLocation = currentLocation;
    }
}