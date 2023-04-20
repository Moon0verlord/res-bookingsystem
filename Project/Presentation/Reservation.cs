 using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

 static class Reservation
 {
     private static AccountModel _acc = null;
     private static MenuLogic _my1DMenu = new MenuLogic();
     private static _2DMenuLogic _my2DMenu = new _2DMenuLogic();
     private static readonly ReservationsLogic Reservations = new ReservationsLogic();
     private static ReservationTableLogic _tableLogic = new ReservationTableLogic();
     private static string userEmail;
     private static int amountOfPeople;
     private static DateTime chosenDate;
     private static (TimeSpan, TimeSpan) chosenTimeslot;
     private static string chosenTable;

     public static void ResStart(AccountModel acc = null)
     {
         _acc = acc;
         userEmail = null;
         amountOfPeople = 0;
         chosenDate = default;
         chosenTimeslot = (default, default);
         chosenTable = "";
         Console.Clear();
         if (acc == null)
         {
             string email = null;
             bool loop = true;
             while (loop)
             {
                 Console.Clear();
                 string prompt = "Vul hier uw e-mail in om een reservatie te maken.";
                 string[] options =
                 {
                     $"Vul hier uw e-mail in" + (email == null ? "\n" : $": {email}\n"), "Doorgaan",
                     "Reservering bekijken", "Ga terug"
                 };
                 int selectedIndex = _my1DMenu.RunMenu(options, prompt);
                 switch (selectedIndex)
                 {
                     case 0:
                         Console.Clear();
                         Console.Write("\n Vul hier uw e-mail in: ");
                         email = Console.ReadLine()!;
                         if (!email.Contains("@") || email.Length < 3)
                         {
                             Console.Clear();
                             Console.ForegroundColor = ConsoleColor.Red;
                             Console.WriteLine(
                                 "\nOnjuiste email.\nEmail moet minimaal een @ hebben en 3 tekens lang zijn.");
                             Console.ResetColor();
                             email = null;
                             Thread.Sleep(3000);
                         }

                         break;
                     case 1:
                         if (email != null)
                         {
                             loop = false;
                             userEmail = email;
                             ResMenu();
                         }
                         else
                         {
                             Console.ForegroundColor = ConsoleColor.Red;
                             Console.Write("\n vul hier uw e-mail in: ");
                             Thread.Sleep(1800);
                             Console.ResetColor();
                         }

                         break;
                     case 2:
                         ViewRes(email);
                         break;
                     case 3:
                         MainMenu.Start();
                         break;
                 }
             }
         }
         else
         {
             userEmail = acc.EmailAddress;
             ResMenu();
         }
         //show available hours on requested date
         //User can select an hour
         //back to menu
     }

     public static void ResMenu()
     {
         Console.Clear();
         amountOfPeople = ChooseGroupSize();
         if (amountOfPeople <= 0)
         {
             if (_acc == null)
                 ResStart();
             else MainMenu.Start();
         }
         else ChooseDate();

         //chosenTimeslot item 1 is time when entering, item 2 is time when leaving.
         while (true)
         {
             Console.ForegroundColor = ConsoleColor.Green;
             Console.Clear();
             Console.WriteLine(
                 $"Email:{userEmail}\nReservatie tafel nummer: {chosenTable}\nDatum: {chosenDate.Date.ToString("dd-MM-yyyy")}" +
                 $"\nTijd: ({chosenTimeslot.Item1} - {chosenTimeslot.Item2})\nWeet u zeker dat u deze tijd wil reserveren? (j/n): ");
             Console.ResetColor();
             string answer = Console.ReadLine()!;
             switch (answer.ToLower())
             {
                 case "ja":
                 case "j":
                     string Res_ID = Reservations.CreateID();
                     Reservations.CreateReservation(userEmail, chosenDate, chosenTable, amountOfPeople,
                         chosenTimeslot.Item1, chosenTimeslot.Item2, Res_ID);
                     Console.Clear();
                     Console.WriteLine("\nReservatie is gemaakt.");
                     Thread.Sleep(1500);
                     UserLogin.DiscardKeys();
                     MainMenu.Start(_acc);
                     break;
                 case "nee":
                 case "n":
                     MainMenu.Start(_acc);
                     break;
                 default:
                     break;
             }
         }
     }

     public static int ChooseGroupSize()
     {
         string amountofPeople = default;
         while (true)
         {
             Console.ResetColor();
             string[] options = new[]
                 { "Vul hier in met hoeveel mensen u komt.", "Ga terug" };
             int selectedIndex = _my1DMenu.RunMenu(options, "Kies hier uw groepsgrootte:");
             Console.Clear();
             switch (selectedIndex)
             {
                 case 0:
                     Console.Write("Typ hier met hoeveel mensen u van plan bent te komen: ");
                     amountofPeople = Console.ReadLine()!;
                     bool success = int.TryParse(amountofPeople, out int number);
                     if (!success)
                     {
                         Console.ForegroundColor = ConsoleColor.Red;
                         Console.WriteLine("Voer enkel geldige nummers in, alstublieft.");
                         Thread.Sleep(2500);
                         amountofPeople = default;
                         break;
                     }
                     else
                     {
                         if (number <= 0)
                         {
                             Console.ForegroundColor = ConsoleColor.Red;
                             Console.WriteLine(
                                 "Nummers kleiner of gelijk aan nul zijn niet toegestaan. Voer opnieuw in.");
                             Thread.Sleep(2500);
                             amountofPeople = default;
                             break;
                         }
                         else if (number > 6)
                         {
                             Console.ForegroundColor = ConsoleColor.Red;
                             Console.WriteLine(
                                 "U moet bellen voor groepsgroottes boven de zes personen, bekijk onze contactinformatie in het hoofdmenu.");
                             Thread.Sleep(2500);
                             amountofPeople = default;
                             break;
                         }
                         else return number;
                     }

                     break;
                 case 1:
                     return 0;
                     break;
             }
         }
     }

     public static void ChooseDate()
     {
         Console.Clear();
         var thisMonth = Reservations.PopulateDates();
         Console.WriteLine(
             $"U kunt alleen een reservatie maken voor de huidige maand ({ReservationsLogic.CurMonth})\nKies een datum (of druk op 'q' om terug te gaan):\n");
         for (int i = 0; i < thisMonth.GetLength(1); i++)
         {
             Console.Write($"{thisMonth[0, i].Date.ToString("ddd", CultureInfo.GetCultureInfo("nl"))}\t");
         }

         DateTime userChoice = _my2DMenu.RunMenu(thisMonth, "", false);
         if (userChoice == default) ResMenu();
         else
         {
             chosenDate = userChoice;
             ChooseTimeslot();
         }
     }

     public static void ChooseTimeslot()
     {
         TimeSpan ts1 = default;
         TimeSpan ts2 = default;
         string[] options = new[] { "16:00 - 18:00", "18:00 - 20:00", "20:00 - 22:00", "Ga terug" };
         int selectedIndex = _my1DMenu.RunMenu(options, "Kies uw gewenste tijdslot:");
         switch (selectedIndex)
         {
             case 0:
                 ts1 = new TimeSpan(0, 16, 0, 0);
                 ts2 = new TimeSpan(0, 18, 0, 0);
                 chosenTimeslot = (ts1, ts2);
                 ChooseTable(chosenDate, chosenTimeslot);
                 break;
             case 1:
                 ts1 = new TimeSpan(0, 18, 0, 0);
                 ts2 = new TimeSpan(0, 20, 0, 0);
                 chosenTimeslot = (ts1, ts2);
                 ChooseTable(chosenDate, chosenTimeslot);
                 break;
             case 2:
                 ts1 = new TimeSpan(0, 20, 0, 0);
                 ts2 = new TimeSpan(0, 22, 0, 0);
                 chosenTimeslot = (ts1, ts2);
                 ChooseTable(chosenDate, chosenTimeslot);
                 break;
             case 3:
                 ChooseDate();
                 break;
         }
     }

     public static void ChooseTable(DateTime res_Date, (TimeSpan, TimeSpan) chosenTime)
     {
         var tablesOnly = Reservations.PopulateTables2D(res_Date, chosenTime);
         // another setwindowsize here to make sure the user didn't make the window too small when choosing other options
         // Picking a setwindowsize under 171 will make the console crash with a bounding error.
         try
         {
             Console.SetWindowSize(180, 35);
         }
         catch
         {
         }

         _tableLogic.TableStart(tablesOnly, amountOfPeople);
         ReservationModel selectedTable = _my2DMenu.RunTableMenu(tablesOnly,
             "  Kies uw tafel (of druk op 'q' om terug te gaan):", amountOfPeople);
         if (selectedTable != default) chosenTable = selectedTable.Id;
         else ChooseTimeslot();
     }

     public static void ViewRes(string Email)
     {
         Console.Clear();
         bool CheckIfRes = false;
         List<ReservationModel> AllRes = AccountsAccess.LoadAllReservations();
         List<string> ReservationsPerson = new();
         List<int> ReservationPersonPositions = new();
         foreach (ReservationModel res in AllRes)
         {
             if (Email == res.EmailAddress && res.Date >= DateTime.Now.Date)
             {
                 ReservationsPerson.Add(
                     $"U heeft een reservering onder de Email: {res.EmailAddress}. Voor tafel {res.Id} en De datum van de resevering is: {res.Date.ToString("dd-MM-yyyy")}. Tijdslot: {res.StartTime} - {res.LeaveTime}");
                 ReservationPersonPositions.Add(AllRes.FindIndex(a => a == res));
                 CheckIfRes = true;
             }
         }

         if (CheckIfRes == false)
         {
             Console.WriteLine("Er staan nog geen reserveringen open met dit email adres.");
             Thread.Sleep(2000);
         }
         else
         {
             ReservationsPerson.Add("Ga terug");
             while (true)
             {
                 var reserv_input = _my1DMenu.RunMenu(ReservationsPerson.ToArray(), "Reserveringen");
                 switch (ReservationsPerson[reserv_input])
                 {
                     case "Ga terug":
                         MainMenu.Start();
                         break;
                     default:

                         Console.Clear();
                         Console.ForegroundColor = ConsoleColor.Red;
                         Console.WriteLine("Wilt u uw reservering annuleren? (j/n)");
                         Console.ResetColor();
                         var Choice = Console.ReadLine();
                         switch (Choice)
                         {
                             case "J":
                             case "j":
                             case "Ja":
                             case "ja":
                                 Console.WriteLine("De reservatie is verwijderd");
                                 AllRes.RemoveAt(ReservationPersonPositions[reserv_input]);
                                 ReservationsPerson.RemoveAt(reserv_input);
                                 AccountsAccess.WriteAllReservations(AllRes);
                                 Thread.Sleep(5000);
                                 UserLogin.DiscardKeys();
                                 break;
                             case "N":
                             case "n":
                             case "Nee":
                             case "nee":
                                 Console.WriteLine("De reservatie is niet verwijderd");
                                 Thread.Sleep(2000);
                                 UserLogin.DiscardKeys();
                                 break;
                             default:
                                 Console.Clear();
                                 Console.ForegroundColor = ConsoleColor.Red;
                                 Console.WriteLine("Incorrecte input");
                                 Thread.Sleep(2000);
                                 Console.ResetColor();
                                 break;
                         }

                         break;
                 }
             }
         }
     }
 }
