using System.Text.RegularExpressions;
public class AnswerLogic
{

    public static bool CheckInput(string answer)
    {
        string pattern = @"^j[a]+$";
        try
        {
            if (Regex.IsMatch(answer.ToLower(),pattern))
            {
                return true;
            }
            return false;
            
        }
        catch (Exception)
        {
            Console.WriteLine("Incorrecte Invoer");
            Thread.Sleep(2000);
            return false;
        }
    }
}