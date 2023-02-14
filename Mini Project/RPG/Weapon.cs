using System.Xml.Schema;

namespace RPG;

public class Weapon
{
    public int ID;
    public string name;
    public string NamePlural;
    public int MinimumDamage;
    public int MaximumDamage;
    public Weapon(int id, string name,
        string nameplural, int minimumDamage, int maximumDamage)
    {
        ID = id;
        this.name = name;
        NamePlural = nameplural;
        MinimumDamage = minimumDamage;
        MaximumDamage = maximumDamage;
    }
}