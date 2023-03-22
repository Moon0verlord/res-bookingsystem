using Newtonsoft.Json.Linq;

namespace Project.Presentation;

using Newtonsoft.Json;


public static class Dishes
{
    // W.I.P - View dishes method
    // doesnt work with full program execute it seperately
    // set json back to DataSources
    
    public static void JsonCursor()
    {
        string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, "Dishes.json"));
        string json = File.ReadAllText(path);
        JObject menu = JObject.Parse(json);
        Console.WriteLine("Dishes:");
        Console.WriteLine("-------");
        Console.WriteLine("Vegan:");
        foreach (var course in menu["vegan"]["2_courses"])
        {
            string appetizer = (string)course["appetizer"];
            string entree = (string)course["entree"];
            Console.WriteLine($"Appetizer: {appetizer}");
            Console.WriteLine($"Entree: {entree}");
        }
    }
}