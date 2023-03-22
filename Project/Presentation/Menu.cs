namespace Project.Presentation;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class Menu
{
    public List<Course> Courses { get; set; }
}

public class Course
{
    public string Appetizer { get; set; }
    public string Soup { get; set; }
    public string Entree { get; set; }
    public string Dessert { get; set; }
}

public void ReadAndDisplayJson(string json)
{
    StreamReader reader = new StreamReader("Dishes.json");
    string jsonString = reader.ReadToEnd();
    reader.Close();
    var Menus = JsonConvert.DeserializeObject<List<Menu>>(json);
    foreach (var item in Menus)
    {
        Console.WriteLine("Menu");
        Console.WriteLine(item.Courses);
    }
}