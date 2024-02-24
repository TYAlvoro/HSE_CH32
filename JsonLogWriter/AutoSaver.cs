using ToolLibrary;

namespace JsonLogWriter;

/// <summary>
/// Класс для автоматической регистрации изменений.
/// </summary>
public static class AutoSaver
{
    // Переменная для хранения времени последнего изменения.
    private static TimeSpan s_previousEventTime = TimeSpan.MinValue;
    
    /// <summary>
    /// Метод, срабатывающий при возникновении события.
    /// </summary>
    /// <param name="sender">Адресант события.</param>
    /// <param name="args">Аргументы события.</param>
    /// <param name="books">Массив книг для записи в файл.</param>
    /// <param name="outputPath">Путь до выходного файла.</param>
    /// <param name="printingImitator">Принтер.</param>
    /// <param name="settings">Настройки.</param>
    public static void DoSomething(object sender, LibraryEventArgs args, Book[] books, 
        string outputPath, PrintingImitator printingImitator, Settings settings)
    {
        // Если разница между событиями <= 15 секунд, то информация записывается в файл.
        // При изменении доступности книги от пользователя прилетает event,
        // после этого прилетает event уже от программы, так как по тз требуется изменить поле должников,
        // в следствие чего каждое изменение доступности влечет за собой регистрацию изменений в файле (фича).
        if (s_previousEventTime != TimeSpan.MinValue)
        {
            TimeSpan timeDifference = args.UpdateTime - s_previousEventTime;
            if (timeDifference.TotalSeconds <= 15)
            {
                Console.ForegroundColor = settings.ColorScheme.ErrorColor;
                printingImitator.Print(settings.ProgramLanguage.EventWrite);
                Console.ForegroundColor = settings.ColorScheme.MainColor;
                JsonTool.WriteJson(books, outputPath, printingImitator, settings);
                Thread.Sleep(1000);
            }
        }
        
        // Обновление времени изменения.
        s_previousEventTime = args.UpdateTime;
    }  
}