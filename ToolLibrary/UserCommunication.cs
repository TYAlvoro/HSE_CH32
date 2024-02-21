namespace ToolLibrary;

/// <summary>
/// Класс, реализующий работу с пользователем.
/// </summary>
public static class UserCommunication
{
    /// <summary>
    /// Метод, получающий от пользователя выбранный пункт меню.
    /// </summary>
    /// <param name="rightBorder">Верхняя граница меню.</param>
    /// <param name="printingImitator"></param>
    /// <param name="settings"></param>
    /// <returns>Выбранный пункт.</returns>
    public static int GetMenuItem(int rightBorder, PrintingImitator printingImitator, Settings settings)
    {
        int itemNumber;
        bool correctInput = false;

        // Пока пользователь не введет корректное числовое значение в диапазоне [1, rightBorder] будет
        // требоваться корректный ввод и выводиться сообщение об ошибке.
        do
        {
            int.TryParse(Console.ReadLine(), out itemNumber);
            
            if (itemNumber < 1 || itemNumber > rightBorder)
            {
                Console.ForegroundColor = settings.ColorScheme.ErrorColor;
                printingImitator.Print($"{settings.ProgramLanguage.WhichNumRequired}{rightBorder}!");
                Console.ForegroundColor = settings.ColorScheme.MainColor;
            }
            else
            {
                correctInput = true;
            }
        } while (!correctInput);

        return itemNumber;
    }

    /// <summary>
    /// Метод, получающий от пользователя путь до файла.
    /// </summary>
    /// <returns>Путь до файла.</returns>
    public static string GetFilePath(PrintingImitator printingImitator, Settings settings)
    {
        bool correctInput = false;
        string? filePath;
        
        // Пока пользователь не введет корректное значение, проверенное методом FileTool.CheckFilePath(),
        // будет требоваться ввод и выводиться сообщение об ошибке.
        do
        {
            filePath = Console.ReadLine();

            try
            {
                //FileTool.CheckFilePath(filePath!);
                correctInput = true;
            }
            catch (Exception ex) when (ex is ArgumentNullException || ex is FileNotFoundException)
            {
                Console.ForegroundColor = settings.ColorScheme.ErrorColor;
                printingImitator.Print(ex.Message);
                Console.ForegroundColor = settings.ColorScheme.MainColor;
            }

        } while (!correctInput);

        return filePath!;
    }
    
    /// <summary>
    /// Метод, получающий от пользователя путь до файла.
    /// </summary>
    /// <returns>Путь до файла.</returns>
    public static string GetUserName(PrintingImitator printingImitator, Settings settings)
    {
        Console.ForegroundColor = settings.ColorScheme.OkColor;
        printingImitator.Print(settings.ProgramLanguage.InputUserName);
        Console.ForegroundColor = settings.ColorScheme.MainColor;
        bool correctInput = false;
        string? userName;
        
        // Пока пользователь не введет корректное значение, проверенное методом FileTool.CheckFilePath(),
        // будет требоваться ввод и выводиться сообщение об ошибке.
        do
        {
            userName = Console.ReadLine();

            if (!string.IsNullOrEmpty(userName))
            {
                correctInput = true;
            }
            else
            {
                Console.ForegroundColor = settings.ColorScheme.ErrorColor;
                printingImitator.Print(settings.ProgramLanguage.UserNameIsIncorrect);
                Console.ForegroundColor = settings.ColorScheme.MainColor;
            }

        } while (!correctInput);

        return userName!;
    }
}