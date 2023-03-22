namespace Project.Presentation;

using Newtonsoft.Json;


static class Menu
{
   public static void ViewMenu()
    {
        StreamReader reader = new("~/DataSources/Dishes.json");
        string json2string = reader.ReadToEnd();
        List<string> listOfObjects = JsonConvert.DeserializeObject<List<string>>(json2string)!;
        reader.Close();
        reader.Dispose();
        Console.WriteLine("The menu is: ");
        foreach (string item in listOfObjects)
        {
            Console.WriteLine(item);
        }
    }
}