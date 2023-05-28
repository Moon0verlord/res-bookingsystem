
public class AnswerLogic
{
    public static Dictionary<List<string>, bool> AnswerValues = new()
    {
        {
             new List<string>
            {
                "J",
                "j",
                "JA",
                "Ja",
                "ja",
                "jaa",
                "Jaaa",
                "jaaa",
                "Jaa",
                "jA",
                "JAA",
                "JAa",
                "JAAA",
                "Jaaaa",
                "JAAAA",
                "JAAAAA",
                "JAJA",
                "jaja",
                "jaJA",
                "JAJAJA",
                "JAja",
                "jAja",
                "jajaja",
                "JaJaJa",
            },true

        },

        {
             new List<string>
            {
                "n",
                "N",
                "NEE",
                "nee",
                "Nee",
                "neee",
                "neeee",
                "Neee",
                "nEE",
                "NEe",
                "NE",
                "Ne",
                "NEeNEe",
                "NEEEEE",
                "NEEEEEE",
                "neenee",
                "neeNEE",
                "NEEneE",
                "neeNEEnee",
                "NEEEEEEEEE",
            },false
        }
    };

    public static bool CheckInput(string answer)
    {
        try
        {
            var Query = (from item in AnswerValues where item.Key.Contains(answer) select item.Value).ToList();
            return Query[0];
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("incorrecte invoer");
            Thread.Sleep(2000);
            return false;
        }
    }

    public static bool Contains(string choice)
    {
        return AnswerValues.Any(x => x.Key.Contains(choice));
    }
}