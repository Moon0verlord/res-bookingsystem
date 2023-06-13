using System.Text.RegularExpressions;
public class AnswerLogic
{
    public static int CheckInput(string answer)
    {
        answer = answer.ToLower();

        string pattern = @"^j[a]+$";
        string pattern2 = @"^n[e]+$";

        switch (answer)
        {
            case "j":
                return 1;
            case "n":
                return 0;
            default:
                if (Regex.IsMatch(answer, pattern))
                {
                    return 1;
                }
                if (Regex.IsMatch(answer, pattern2))
                {
                    return 0;
                }
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Incorrecte Invoer");
                Thread.Sleep(2000);
                UserLogin.DiscardKeys();
                Console.ResetColor();
                return -1;
        }
    }
}