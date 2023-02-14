namespace RPG;

public class Player
{
    string Name;
    int MaxHP;
    int gold;
    int XP;
    int level;
    Weapon CurrentWeapon;
    Location CurrentLocation;
    
    public Player(string name)
    {
        Name = name;
        MaxHP = 100;
    }
}