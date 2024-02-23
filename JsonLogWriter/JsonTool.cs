using System.Text.Json;
using ToolLibrary;

namespace JsonLogWriter;

public static class JsonTool
{
    public static Book[] CheckFile(PrintingImitator printingImitator, Settings settings, out string fileName)
    {
        bool correctFile = false;
        Book[] books = Array.Empty<Book>();

        do
        {
            string filePath = UserCommunication.GetFilePath(printingImitator, settings, true);
            fileName = Path.GetFileName(filePath);
            string jsonString = File.ReadAllText(filePath);
            
            try
            {
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
    private static void CheckBooks(Book[] books, Settings settings)
    {
        foreach (var book in books)
        {
            if (!(!string.IsNullOrEmpty(book.BookId) && !string.IsNullOrEmpty(book.Title) &&
                  !string.IsNullOrEmpty(book.Author) && book.IsAvailable is not null
                  && !string.IsNullOrEmpty(book.Genre) && book.PublicationYear is not null &&
                  book.Borrowers is not null))
            {
                throw new ArgumentException(settings.ProgramLanguage.BookFieldsCantBeNull + book.Title);
            }

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
}