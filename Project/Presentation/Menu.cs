namespace Project.Presentation;

using Newtonsoft.Json;


static class Menu
{
   public static void ViewMenu()
    {
        string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/Dishes.json"));
        StreamReader reader = new(path);
        string json2string = reader.ReadToEnd();
        // change list of objects to jsoon array
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