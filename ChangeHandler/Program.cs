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
        MenuPrinter.ShowGreeting(printingImitator, settings);
    } 
}