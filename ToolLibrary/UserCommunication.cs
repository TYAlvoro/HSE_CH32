using System.ComponentModel;

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
    /// <param name="printingImitator">Объект принтера.</param>
    /// <param name="settings">Объект настроек.</param>
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
    /// <param name="printingImitator">Объект принтера.</param>
    /// <param name="settings">Объект настроек.</param>
    /// <param name="file">Нужно ли проверить файл или директорию.</param>
    /// <returns>Путь до файла.</returns>
    public static string GetFilePath(PrintingImitator printingImitator, Settings settings, bool file)
    {
        bool correctInput = false;
        string? path;
        
        // Пока пользователь не введет корректное значение, проверенное методом FileTool.CheckFilePath(),
        // будет требоваться ввод и выводиться сообщение об ошибке.
        do
        {
            path = Console.ReadLine();

            try
            {
                if (file)
                    FileTool.CheckFilePath(path, settings);
                else
                    FileTool.CheckDirectoryPath(path, settings);
                correctInput = true;
            }
            catch (Exception ex) when (ex is ArgumentNullException || ex is FileNotFoundException || ex is DirectoryNotFoundException)
            {
                Console.ForegroundColor = settings.ColorScheme.ErrorColor;
                printingImitator.Print(ex.Message);
                Console.ForegroundColor = settings.ColorScheme.MainColor;
            }

        } while (!correctInput);

        return path!;
    }
    
    /// <summary>
    /// Метод, получающий юзернейм.
    /// </summary>
    /// <param name="printingImitator">Объект принтера.</param>
    /// <param name="settings">Объект настроек.</param>
    /// <returns>Юзернейм.</returns>
    public static string GetUserName(PrintingImitator printingImitator, Settings settings)
    {
        Console.ForegroundColor = settings.ColorScheme.OkColor;
        printingImitator.Print(settings.ProgramLanguage.InputUserName);
        Console.ForegroundColor = settings.ColorScheme.MainColor;
        bool correctInput = false;
        string? userName;
        
        // Пока пользователь не введет корректное значение, будет требоваться ввод и выводиться сообщение об ошибке.
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
    
    /// <summary>
    /// Метод, получающий от пользователя название книги.
    /// </summary>
    /// <param name="printingImitator">Объект принтера.</param>
    /// <param name="settings">Объект настроек.</param>
    /// <param name="bookTitles">Названия книг.</param>
    /// <returns>Нужное пользователю название.</returns>
    public static string GetBookTitle(PrintingImitator printingImitator, Settings settings, string[] bookTitles)
    {
        bool correctInput = false;
        string? title;
        
        // Пока пользователь не введет корректное значение, будет требоваться ввод и выводиться сообщение об ошибке.
        do
        {
            title = Console.ReadLine();

            if (!string.IsNullOrEmpty(title) && Array.Exists(bookTitles, element => element == title))
            {
                correctInput = true;
            }
            else
            {
                Console.ForegroundColor = settings.ColorScheme.ErrorColor;
                printingImitator.Print(settings.ProgramLanguage.TitleIsIncorrect);
                Console.ForegroundColor = settings.ColorScheme.MainColor;
            }

        } while (!correctInput);

        return title!;
    }
    
    /// <summary>
    /// Метод, получающий от пользователя значение для изменения поля.
    /// </summary>
    /// <param name="printingImitator">Объект принтера.</param>
    /// <param name="settings">Объект настроек.</param>
    /// <param name="isInt">Целочисленное ли значение.</param>
    /// <param name="isBool">Булевое ли значение.</param>
    /// <returns>Значение для изменения.</returns>
    public static object GetValue(PrintingImitator printingImitator, Settings settings, bool isInt, bool isBool)
    {
        bool correctInput = false;
        string? value;
        
        // Пока пользователь не введет корректное значение, будет требоваться ввод и выводиться сообщение об ошибке.
        do
        {
            value = Console.ReadLine();
            
            // Проверка на совместимость нужного и введенного типов данных.
            if (string.IsNullOrEmpty(value) || 
                (isInt && !int.TryParse(value, out int intValue)) || 
                (isBool && !bool.TryParse(value, out bool boolValue)))
            {
                Console.ForegroundColor = settings.ColorScheme.ErrorColor;
                printingImitator.Print(settings.ProgramLanguage.IncorrectValue);
                Console.ForegroundColor = settings.ColorScheme.MainColor;
            }
            else
            {
                correctInput = true;
            }

        } while (!correctInput);

        // Если нужно было целочисленное, то оно и вернется.
        if (isInt)
        {
            int.TryParse(value, out int intValue);
            return intValue;
        }
        
        // Если нужно было булевое, то оно и вернется.
        if (isBool)
        {
            bool.TryParse(value, out bool boolValue);
            return boolValue;
        }
        
        return value!;
    }
}