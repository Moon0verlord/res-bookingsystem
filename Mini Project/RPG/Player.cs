namespace RPG;

public class Player
{
    public string Name;
    public static int MaxHP;
    public static int CurrentHP;
    public static int Gold;
    public static int XP;
    public static int Level;
    public static Weapon CurrentWeapon;
    public static Location CurrentLocation;
    
    public Player(string Name, int MaxHP, int currentHP, int gold,
        int xp, int level, Weapon currentWeapon, Location currentLocation)
    {
        this.Name = Name;
        MaxHP = 100;
        CurrentHP = currentHP;
        Gold = gold;
        XP = xp;
        Level = level;
        CurrentWeapon = currentWeapon;
        CurrentLocation = currentLocation;
    }
}