using JsonLogWriter;
using ToolLibrary;

namespace MenuLibrary;

/// <summary>
/// Класс, реализующий практически все выводы в консоль. Почти полностью решает проблему "побочного эффекта".
/// </summary>
public static class MenuPrinter
{
    /// <summary>
    /// Вывод приветственной информации.
    /// </summary>
    public static void ShowGreeting(PrintingImitator printingImitator, Settings settings)
    {
        Console.ForegroundColor = settings.ColorScheme.MainColor;
        printingImitator.Print(settings.ProgramLanguage.Greeting + settings.UserName);
        printingImitator.Print(settings.ProgramLanguage.ProgramDestiny);
        Thread.Sleep(1000);
        Console.Clear();
        Console.ForegroundColor = settings.ColorScheme.QuestionColor;
        printingImitator.Print(settings.ProgramLanguage.WantToConfigure);
        
        Console.ForegroundColor = settings.ColorScheme.FirstColor;
        printingImitator.Print(settings.ProgramLanguage.MenuYes);
        Console.ForegroundColor = settings.ColorScheme.SecondColor;
        printingImitator.Print(settings.ProgramLanguage.MenuNo);
        Console.ForegroundColor = settings.ColorScheme.MainColor;

        int itemNumber = UserCommunication.GetMenuItem(2, printingImitator, settings);

        if (itemNumber == 1)
        {
            Console.Clear();
            SetSettings(printingImitator, settings);
        }

        Console.Clear();
    }

    public static (Book[], string) ShowInformation(PrintingImitator printingImitator, Settings settings)
    {
        Console.ForegroundColor = settings.ColorScheme.MainColor;
        printingImitator.Print(settings.UserName + settings.ProgramLanguage.LetsStart);
        Console.ForegroundColor = settings.ColorScheme.OkColor;
        printingImitator.Print(settings.ProgramLanguage.EnterInputFilePath); 
        Book[] books = JsonTool.CheckFile(printingImitator, settings, out string fileName);
        Console.Clear();
        Console.ForegroundColor = settings.ColorScheme.OkColor;
        printingImitator.Print(settings.ProgramLanguage.EnterOutputFilePath);
        string outputDirectoryPath = UserCommunication.GetFilePath(printingImitator, settings, false);
        fileName = fileName.Split(".")[0] + "_tmp.json";
        string outputPath = Path.Combine(outputDirectoryPath, fileName);
        File.Create(outputPath);

        Console.ForegroundColor = settings.ColorScheme.MainColor;
        printingImitator.Print(settings.ProgramLanguage.CreatedOutputFile + outputPath);
        
        Thread.Sleep(1500);
        Console.Clear();
        
        return (books, outputPath);
    }

    private static void SetSettings(PrintingImitator printingImitator, Settings settings)
    {   
        ChangeUserName(printingImitator, settings);
        ChangeColorScheme(printingImitator, settings);
        ChangeLanguage(printingImitator, settings);
        ChangeDelay(printingImitator, settings);
        printingImitator.Print(settings.ProgramLanguage.AllChangesAccepted);
        FileTool.CreateFile("settings.dat");
        FileTool.WriteSettingsToFile(settings);
        Environment.Exit(0);
    }

    private static void ChangeUserName(PrintingImitator printingImitator, Settings settings)
    {
        Console.ForegroundColor = settings.ColorScheme.MainColor;
        printingImitator.Print(settings.ProgramLanguage.WantToChangeName);
        Console.ForegroundColor = settings.ColorScheme.FirstColor;
        printingImitator.Print(settings.ProgramLanguage.MenuYes);
        Console.ForegroundColor = settings.ColorScheme.SecondColor;
        printingImitator.Print(settings.ProgramLanguage.MenuNo);
        Console.ForegroundColor = settings.ColorScheme.MainColor;
        
        int itemNumber = UserCommunication.GetMenuItem(2, printingImitator, settings);

        if (itemNumber == 1)
        {
            settings.UserName = UserCommunication.GetUserName(printingImitator, settings);
            printingImitator.Print($"{settings.ProgramLanguage.NameWasChanged}{settings.UserName}");
        } 
    }
    
    private static void ChangeColorScheme(PrintingImitator printingImitator, Settings settings)
    {
        Console.ForegroundColor = settings.ColorScheme.MainColor;
        printingImitator.Print(settings.ProgramLanguage.WantToChangeColorScheme);
        Console.ForegroundColor = settings.ColorScheme.FirstColor;
        printingImitator.Print(settings.ProgramLanguage.MenuYes);
        Console.ForegroundColor = settings.ColorScheme.SecondColor;
        printingImitator.Print(settings.ProgramLanguage.MenuNo);
        Console.ForegroundColor = settings.ColorScheme.MainColor;
        
        int itemNumber = UserCommunication.GetMenuItem(2, printingImitator, settings);

        if (itemNumber == 1)
        {
            Console.ForegroundColor = settings.ColorScheme.FirstColor;
            printingImitator.Print(settings.ProgramLanguage.MenuColdTheme);
            Console.ForegroundColor = settings.ColorScheme.SecondColor;
            printingImitator.Print(settings.ProgramLanguage.MenuMainTheme);
            Console.ForegroundColor = settings.ColorScheme.MainColor;
            itemNumber = UserCommunication.GetMenuItem(2, printingImitator, settings);
            if (itemNumber == 1)
            {
                settings.ColorScheme = new ColdColorScheme();
            }
            else
            {
                settings.ColorScheme = new MainColorScheme();
            }
            
            printingImitator.Print(settings.ProgramLanguage.ChangesAccepted);
        } 
    }
    
    private static void ChangeLanguage(PrintingImitator printingImitator, Settings settings)
    {
        Console.ForegroundColor = settings.ColorScheme.MainColor;
        printingImitator.Print(settings.ProgramLanguage.WantToChangeLanguage);
        Console.ForegroundColor = settings.ColorScheme.FirstColor;
        printingImitator.Print(settings.ProgramLanguage.MenuYes);
        Console.ForegroundColor = settings.ColorScheme.SecondColor;
        printingImitator.Print(settings.ProgramLanguage.MenuNo);
        Console.ForegroundColor = settings.ColorScheme.MainColor;
        
        int itemNumber = UserCommunication.GetMenuItem(2, printingImitator, settings);

        if (itemNumber == 1)
        {
            Console.ForegroundColor = settings.ColorScheme.FirstColor;
            printingImitator.Print(settings.ProgramLanguage.MenuRussian);
            Console.ForegroundColor = settings.ColorScheme.SecondColor;
            printingImitator.Print(settings.ProgramLanguage.MenuEnglish);
            Console.ForegroundColor = settings.ColorScheme.MainColor;
            itemNumber = UserCommunication.GetMenuItem(2, printingImitator, settings);
            if (itemNumber == 1)
            {
                settings.ProgramLanguage = new MainLanguage();
            }
            else
            {
                settings.ProgramLanguage = new EnglishLanguage();
            }
            
            printingImitator.Print(settings.ProgramLanguage.ChangesAccepted);
        } 
    }
    
    private static void ChangeDelay(PrintingImitator printingImitator, Settings settings)
    {
        Console.ForegroundColor = settings.ColorScheme.MainColor;
        printingImitator.Print(settings.ProgramLanguage.WantToChangeDelay);
        Console.ForegroundColor = settings.ColorScheme.FirstColor;
        printingImitator.Print(settings.ProgramLanguage.MenuYes);
        Console.ForegroundColor = settings.ColorScheme.SecondColor;
        printingImitator.Print(settings.ProgramLanguage.MenuNo);
        Console.ForegroundColor = settings.ColorScheme.MainColor;
        
        int itemNumber = UserCommunication.GetMenuItem(2, printingImitator, settings);

        if (itemNumber == 1)
        {
            Console.ForegroundColor = settings.ColorScheme.FirstColor;
            printingImitator.Print(settings.ProgramLanguage.MenuSlow);
            Console.ForegroundColor = settings.ColorScheme.SecondColor;
            printingImitator.Print(settings.ProgramLanguage.MenuMedium);
            Console.ForegroundColor = settings.ColorScheme.ThirdColor;
            printingImitator.Print(settings.ProgramLanguage.MenuFast);
            Console.ForegroundColor = settings.ColorScheme.MainColor;
            itemNumber = UserCommunication.GetMenuItem(3, printingImitator, settings);
            if (itemNumber == 1)
            {
                settings.PrintDelay = 100;
            }
            else if (itemNumber == 2)
            {
                settings.PrintDelay = 30;
            }
            else
            {
                settings.PrintDelay = 15;
            }

            printingImitator.Delay = settings.PrintDelay;
            printingImitator.Print(settings.ProgramLanguage.ChangesAccepted);
        } 
    }
}