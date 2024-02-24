using System.Text;
using System.Text.Json;
using ToolLibrary;

namespace JsonLogWriter;

/// <summary>
/// Класс для удобной работы с JSON файлом.
/// </summary>
public static class JsonTool
{
    /// <summary>
    /// Проверка JSON файла на корректность.
    /// </summary>
    /// <param name="printingImitator">Объект принтера.</param>
    /// <param name="settings">Объект настроек.</param>
    /// <param name="fileName">Путь до входного файла.</param>
    /// <returns>Массив объектов книг.</returns>
    public static Book[] CheckFile(PrintingImitator printingImitator, Settings settings, out string fileName)
    {
        bool correctFile = false;
        Book[] books = Array.Empty<Book>();

        // Повторение ввода до появления корректного.
        do
        {
            // Считывание и проверка корректности пути до файла.
            string filePath = UserCommunication.GetFilePath(printingImitator, settings, true);
            fileName = Path.GetFileName(filePath);
            string jsonString = File.ReadAllText(filePath);
            
            try
            {
                // Десериализация и проверка каждого объекта книги на корректность.
                books = JsonSerializer.Deserialize<Book[]>(jsonString)!;
                CheckBooks(books, settings);
                correctFile = true;
            }
            catch (Exception ex) when (ex is ArgumentException || ex is JsonException)
            {
                Console.ForegroundColor = settings.ColorScheme.ErrorColor;
                printingImitator.Print(ex.Message);
                Console.ForegroundColor = settings.ColorScheme.MainColor;
            }
            
        } while (!correctFile);
        
        return books;
    }
    
    /// <summary>
    /// Класс для проверки каждого экземпляра книги на корректность.
    /// </summary>
    /// <param name="books">Массив объектов книг.</param>
    /// <param name="settings">Объект настроек.</param>
    /// <exception cref="ArgumentException">Если хоть одно поле отсутствует или пустое.</exception>
    private static void CheckBooks(Book[] books, Settings settings)
    {
        // Проверка каждого поля книги на пустоту.
        foreach (var book in books)
        {
            if (!(!string.IsNullOrEmpty(book.BookId) && !string.IsNullOrEmpty(book.Title) &&
                  !string.IsNullOrEmpty(book.Author) && book.IsAvailable is not null
                  && !string.IsNullOrEmpty(book.Genre) && book.PublicationYear is not null &&
                  book.Borrowers is not null))
            {
                throw new ArgumentException(settings.ProgramLanguage.BookFieldsCantBeNull + book.Title);
            }

            // Проверка каждого поля должника на пустоту.
            foreach (var borrower in book.Borrowers)
            {
                if (!(!string.IsNullOrEmpty(borrower.BorrowerId) && !string.IsNullOrEmpty(borrower.BorrowerName) &&
                      !string.IsNullOrEmpty(borrower.BorrowDate) && !string.IsNullOrEmpty(borrower.DueDate)))
                {
                    throw new ArgumentException(settings.ProgramLanguage.BorrowerFieldsCantBeNull + borrower.BorrowerName);
                }
            }
        }
    }

    /// <summary>
    /// Запись информации в файл.
    /// </summary>
    /// <param name="books">Массив объектов книг.</param>
    /// <param name="filePath">Путь до выходного файла.</param>
    /// <param name="printingImitator">Объект принтера.</param>
    /// <param name="settings">Объект настроек.</param>
    public static void WriteJson(Book[] books, string filePath, PrintingImitator printingImitator, Settings settings)
    {
        // "Красивый" JSON формат.
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string jsonString = JsonSerializer.Serialize(books, options);

        // Безопасная запись в файл.
        try
        {
            using (StreamWriter streamWriter = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                streamWriter.WriteLine(jsonString);
            }
        }
        catch (IOException ex)
        {
            Console.ForegroundColor = settings.ColorScheme.ErrorColor;
            printingImitator.Print(ex.Message);
            Console.ForegroundColor = settings.ColorScheme.MainColor;
        }
    }
}