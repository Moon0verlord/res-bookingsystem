using System;

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
        string garden = World.LocationByID(World.LOCATION_ID_ALCHEMISTS_GARDEN).Abbreviation;
        string town = World.LocationByID(World.LOCATION_ID_TOWN_SQUARE).Abbreviation;
        string farmer = World.LocationByID(World.LOCATION_ID_FARMHOUSE).Abbreviation;
        string field = World.LocationByID(World.LOCATION_ID_FARM_FIELD).Abbreviation;
        string hut = World.LocationByID(World.LOCATION_ID_ALCHEMIST_HUT).Abbreviation;
        string guard = World.LocationByID(World.LOCATION_ID_GUARD_POST).Abbreviation;
        string bridge = World.LocationByID(World.LOCATION_ID_BRIDGE).Abbreviation;
        string spider = World.LocationByID(World.LOCATION_ID_SPIDER_FIELD).Abbreviation;
        string house = World.LocationByID(World.LOCATION_ID_HOME).Abbreviation;
        string map = "\n" + String.Format("{0, 5}", garden) + "\n" + String.Format("{0, 5}", hut) + "\n" 
                     + "  " + field + farmer + town + guard + bridge + spider + "\n" + "    " + house + "\n";
        return $"Current Location: {Abbreviation}\nGame map:\n {map}";
    }

    public string Compass()
    {
        string north = (LocationToNorth != null) ? "    N\n    |" : "";
        string east = (LocationToEast != null) ? "---E" : "";
        string south = (LocationToSouth != null) ? "    |\n    S" : "";
        string west = (LocationToWest != null) ? "W---" : "    ";
        return "\n" + north + "\n" + west + "|" + east + "\n" + south;
    }
}