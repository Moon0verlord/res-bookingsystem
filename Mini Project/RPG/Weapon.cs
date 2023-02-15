using System.Xml.Schema;

namespace RPG;

public class Weapon
{
    public int ID;
    public string Name;
    public string NamePlural;
    public int MinimumDamage;
    public int MaximumDamage;
    public Weapon(int id, string name,
        string nameplural, int minimumDamage, int maximumDamage)
    {
        ID = id;
        Name = name;
        NamePlural = nameplural;
        MinimumDamage = minimumDamage;
        MaximumDamage = maximumDamage;
    }
}