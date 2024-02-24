using JsonLogWriter;
using MenuLibrary;
using ToolLibrary;

namespace ChangeHandler;

/// <summary>
/// Главный класс программы.
/// </summary>
internal class Program
{
    /// <summary>
    /// Точка вхоода в программу.
    /// </summary>
    private static void Main()
    {
        // Инициализация настроек и принтера.
        // При первом запуске создаются дефолтные настройки, которые и считываются.
        Settings settings = FileTool.OpenFileAndSetSettings();
        PrintingImitator printingImitator = new PrintingImitator(settings.PrintDelay);
        
        MenuPrinter.ShowGreeting(printingImitator, settings);
        // Получение массива книг и выходного файла для автоматической регистрации изменений.
         (Book[] books, string outputPath) = MenuPrinter.ShowInformation(printingImitator, settings);

        // Все книги и должники подписываются на событие.
        foreach (var book in books)
        {
            book.Updated += (sender, args) => AutoSaver.DoSomething(sender, args, books, 
                outputPath, printingImitator, settings);
            foreach (var borrower in book.Borrowers!)
            {
                borrower.Updated += (sender, args) => AutoSaver.DoSomething(sender, args, books, 
                    outputPath, printingImitator, settings);;
            }
        }
        
        // Показ основого меню с выбором действий.
        MenuPrinter.ShowActionMenu(printingImitator, settings, books);
    } 
}