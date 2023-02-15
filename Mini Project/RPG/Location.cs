namespace RPG;

public class Location
{
    public int ID;
    public string Name;
    public string Description;
    public Item ItemRequiredToEnter;
    public Quest QuestAvailableHere;
    public Monster MonsterLivingHere;
    public Location LocationToNorth;
    public Location LocationToEast;
    public Location LocationToSouth;
    public Location LocationToWest;
    public string Abbreviation;

    public Location(int id, string name, string desc, Item item, Quest quest, Monster monster, string abb)
    {
        ID = id;
        Name = name;
        Description = desc;
        ItemRequiredToEnter = item;
        QuestAvailableHere = quest;
        MonsterLivingHere = monster;
        Abbreviation = abb;
    }

    public string Map()
    {
        return "";
    }
}