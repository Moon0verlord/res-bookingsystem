﻿namespace RPG;

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
        string north = (LocationToNorth == null) ? "" : LocationToNorth.Abbreviation;
        string east = (LocationToEast == null) ? "" : LocationToEast.Abbreviation;
        string south = (LocationToSouth == null) ? "" : LocationToSouth.Abbreviation;
        string west = (LocationToWest == null) ? "    " : LocationToWest.Abbreviation;
        return $"Current Location: {Abbreviation}\nGame map: \n" + "\n" + "    " + north + "\n" + west + Abbreviation + east + "\n" + south;
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