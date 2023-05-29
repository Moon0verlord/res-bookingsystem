using System.Text.RegularExpressions;
public class AnswerLogic
{

    public static int CheckInput(string answer)
    {
        //1 is correct output
        //0 is false output
        //-1 is error
        string pattern = @"^j[a]+$";
        string pattern2 = @"^n[e]+$";
       
            if (Regex.IsMatch(answer.ToLower(),pattern))
            {
                return 1;
            } 
            if (Regex.IsMatch(answer.ToLower(),pattern2))
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