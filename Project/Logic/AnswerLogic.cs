
public class AnswerLogic
{
    public Dictionary<List<string>, bool> AnswerValues = new()
    {
        {
             new List<string>
            {
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

    public bool CheckInput(string answer)
    {
        var Query = (from item in AnswerValues where item.Key.Contains(answer) select item.Value).ToList();
        return Query[0];
    }
}