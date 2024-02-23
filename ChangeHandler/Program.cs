using System.Text.Json;
using JsonLogWriter;
using MenuLibrary;
using ToolLibrary;

namespace ChangeHandler;

internal class Program
{
    private static void Main()
    {
        Settings settings = FileTool.OpenFileAndSetSettings();
        PrintingImitator printingImitator = new PrintingImitator(settings.PrintDelay);
        /*
        string jsonString = File.ReadAllText("D:\\RiderProjects\\ChangeHandler\\JsonLogWriter\\test.json");

        try
        {
            Book[] books = JsonSerializer.Deserialize<Book[]>(jsonString)!;
            JsonTool.CheckBooks(books, settings);
        }
        catch (Exception ex) when (ex is ArgumentException || ex is JsonException)
        {
            printingImitator.Print(ex.Message);
        }
        */

        MenuPrinter.ShowInformation(printingImitator, settings);

    } 
}