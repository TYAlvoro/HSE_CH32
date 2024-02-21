namespace ToolLibrary;

public class EnglishLanguage : MainLanguage
{
    public override string Greeting => "Hello, ";
    public override string ProgramDestiny => "Here you can monitor the availability of books in the library.";
    public override string WantToConfigure => "Would you like to set up the program before using it?";
    public override string WhichNumRequired => "Must be entered a number from 1 to ";
    public override string MenuYes => "1. Yes";
    public override string MenuNo => "2. No";
    public override string WantToChangeName =>
        "Do you want to specify how to contact you? Example: The Dark Lord, Oberleutnant Baumann, etc.";
    public override string UserNameIsIncorrect => "The username cannot be empty!";
    public override string InputUserName => "Enter the user name...";
    public override string NameWasChanged => "The user's name has been changed to: ";
    public override string WantToChangeColorScheme => "Do you want to choose the color scheme of the application?";
    public override string ChangesAccepted => "Changes was accepted";
    public override string MenuColdTheme => "1. Cold scheme";
    public override string MenuMainTheme => "2. Warm scheme";
    public override string WantToChangeLanguage => "Do you want to choose the language of the application?";
    public override string WantToChangeDelay => "Do you want to choose the print speed in the app?";
    public override string MenuSlow => "1. Slow";
    public override string MenuMedium => "2. Medium";
    public override string MenuFast => "3. Fast";
    public override string AllChangesAccepted => "All changes have been accepted, restart the application to apply!";
    public override string LetsStart =>
        $", let's get started!{Environment.NewLine}You are provided with convenient software for working with library data. " +
        $"Here you can filter the list of books by various criteria,{Environment.NewLine}change information about books " +
        $"and notify people on the waiting list about the possibility of taking a particular book.";
    public override string EnterInputFilePath =>
        "For the program to work correctly, enter the path to the library data file..."; 
    public override string EnterOutputFilePath =>
        "For the program to work correctly, enter the path to the folder for the output data. The output file will be created automatically..."; 
    public override string FilePathIsEmpty => "The resulting file path is empty!";
    public override string FileNotFound => "The file in the specified path does not exist!";
    public override string DirectoryPathIsEmpty => "The resulting directory path is empty!";
    public override string DirectoryNotFound => "The directory in the specified path does not exist!";

    public override string ToString()
    {
        return "English";
    }
}