namespace ToolLibrary;

/// <summary>
/// Класс для хранения фраз на английском языке.
/// </summary>
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
    public override string BookFieldsCantBeNull => "No field in the description of the book can be missing! Book:";
    public override string BorrowerFieldsCantBeNull => "Not a single field in the waiting questionnaire can be missing! Name:";
    public override string CreatedOutputFile => "An output file has been created along the path: ";
    public override string WhatCanDo => "Choose what you want to do, ";
    public override string MenuSortId => "1. Sort the list of books by Id.";
    public override string MenuSortTitle => "2. Sort the list of books by title.";
    public override string MenuSortAuthor => "3. Sort the list of books by author's name.";
    public override string MenuSortYear => "4. Sort the list of books by year of publication.";
    public override string MenuSortGenre => "5. Sort the list of books by genre.";
    public override string MenuSortAvailable => "6. Sort the list of books by availability level.";
    public override string MenuChangeData => "7. Change one of the characteristics in the book.";
    public override string ChooseBook => "First, enter the name of the book you want to work with.";
    public override string TitleIsIncorrect => "Incorrect title of the book!";
    public override string IncorrectValue => "Incorrect value!";
    public override string ChooseItem => "Select the value to edit";
    public override string Title => "1. Title";
    public override string Author => "2. Author";
    public override string Year => "3. Year";
    public override string Genre => "4. Genre";
    public override string Available => "5. Available";
    public override string InputValue => "Enter a new value...";
    public override string EventWrite => "The values changed quickly, so the changes are recorded in the output file.";
    public override string WantToSave => "Would you like to save the changes to a file?";
    public override string ChangesSaves => "The changes are saved to the specified file.";
    public override string EnterOutputPath => "Enter the path to the file.";
    public override string Quit => "8. Quit.";



    public override string ToString()
    {
        return "English";
    }
}