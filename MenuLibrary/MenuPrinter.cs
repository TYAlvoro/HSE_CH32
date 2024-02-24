using JsonLogWriter;
using ToolLibrary;

namespace MenuLibrary;

/// <summary>
/// Класс, реализующий практически все выводы в консоль. Почти полностью решает проблему "побочного эффекта".
/// </summary>
public static class MenuPrinter
{
    /// <summary>
    /// Вывод приветственной информации.
    /// </summary>
    /// <param name="printingImitator">Объект принтера.</param>
    /// <param name="settings">Объект настроек.</param>
    public static void ShowGreeting(PrintingImitator printingImitator, Settings settings)
    {
        Console.ForegroundColor = settings.ColorScheme.MainColor;
        printingImitator.Print(settings.ProgramLanguage.Greeting + settings.UserName);
        printingImitator.Print(settings.ProgramLanguage.ProgramDestiny);
        Thread.Sleep(1000);
        Console.Clear();
        Console.ForegroundColor = settings.ColorScheme.QuestionColor;
        printingImitator.Print(settings.ProgramLanguage.WantToConfigure);
        
        Console.ForegroundColor = settings.ColorScheme.FirstColor;
        printingImitator.Print(settings.ProgramLanguage.MenuYes);
        Console.ForegroundColor = settings.ColorScheme.SecondColor;
        printingImitator.Print(settings.ProgramLanguage.MenuNo);
        Console.ForegroundColor = settings.ColorScheme.MainColor;

        // Проверка и запуск конфигурации (если нужно).
        int itemNumber = UserCommunication.GetMenuItem(2, printingImitator, settings);

        if (itemNumber == 1)
        {
            Console.Clear();
            SetSettings(printingImitator, settings);
        }

        Console.Clear();
    }

    /// <summary>
    /// Вывод дальнейшей информации и получение всех необходимых объектов.
    /// </summary>
    /// <param name="printingImitator">Объект принтера.</param>
    /// <param name="settings">Объект настроек.</param>
    /// <returns>Массив книг и путь до выходного файла.</returns>
    public static (Book[], string) ShowInformation(PrintingImitator printingImitator, Settings settings)
    {
        Console.ForegroundColor = settings.ColorScheme.MainColor;
        printingImitator.Print(settings.UserName + settings.ProgramLanguage.LetsStart);
        Console.ForegroundColor = settings.ColorScheme.OkColor;
        printingImitator.Print(settings.ProgramLanguage.EnterInputFilePath); 
        Book[] books = JsonTool.CheckFile(printingImitator, settings, out string fileName);
        Console.Clear();
        Console.ForegroundColor = settings.ColorScheme.OkColor;
        printingImitator.Print(settings.ProgramLanguage.EnterOutputFilePath);
        string outputDirectoryPath = UserCommunication.GetFilePath(printingImitator, settings, false);
        string outputPath = Path.Combine(outputDirectoryPath, fileName.Split(".")[0] + "_tmp.json");
        FileTool.CreateFile(outputPath);

        Console.ForegroundColor = settings.ColorScheme.MainColor;
        printingImitator.Print(settings.ProgramLanguage.CreatedOutputFile + outputPath);
        
        Thread.Sleep(1500);
        Console.Clear();
        
        return (books, outputPath);
    }
    
    /// <summary>
    /// Выбор пользователем, какую именно книгу он будет редактировать.
    /// </summary>
    /// <param name="printingImitator">Объект принтера.</param>
    /// <param name="settings">Объект настроек.</param>
    /// <param name="books">Массив книг.</param>
    /// <returns>Книга для изменения полей.</returns>
    public static Book ShowListOfTitles(PrintingImitator printingImitator, Settings settings, Book[] books)
    {
        Console.Clear();
        
        // Массив названий.
        string[] titles = new string[books.Length];

        Console.ForegroundColor = settings.ColorScheme.OkColor;
        for (int i = 0; i < books.Length; i++)
        {
            titles[i] = books[i].Title;
            Console.WriteLine(books[i].Title);
        }
        
        Console.WriteLine();
        Console.ForegroundColor = settings.ColorScheme.MainColor;
        printingImitator.Print(settings.ProgramLanguage.ChooseBook);
        
        // Получение корректного названия книги от пользователя.
        string title = UserCommunication.GetBookTitle(printingImitator, settings, titles);

        return books[Array.IndexOf(titles, title)];
    }

    /// <summary>
    /// Меню с выбором действий и создание этих действий.
    /// </summary>
    /// <param name="printingImitator">Объект принтера.</param>
    /// <param name="settings">Объект настроек.</param>
    /// <param name="books">Массив книг.</param>
    public static void ShowActionMenu(PrintingImitator printingImitator, Settings settings, Book[] books)
    {
        Console.Clear();
        Console.ForegroundColor = settings.ColorScheme.MainColor;
        printingImitator.Print(settings.ProgramLanguage.WhatCanDo + settings.UserName);
        Console.ForegroundColor = settings.ColorScheme.FirstColor;
        printingImitator.Print(settings.ProgramLanguage.MenuSortId);
        Console.ForegroundColor = settings.ColorScheme.SecondColor;
        printingImitator.Print(settings.ProgramLanguage.MenuSortTitle);
        Console.ForegroundColor = settings.ColorScheme.ThirdColor;
        printingImitator.Print(settings.ProgramLanguage.MenuSortAuthor);
        Console.ForegroundColor = settings.ColorScheme.FirstColor;
        printingImitator.Print(settings.ProgramLanguage.MenuSortYear);
        Console.ForegroundColor = settings.ColorScheme.SecondColor;
        printingImitator.Print(settings.ProgramLanguage.MenuSortGenre);
        Console.ForegroundColor = settings.ColorScheme.ThirdColor;
        printingImitator.Print(settings.ProgramLanguage.MenuSortAvailable);
        Console.ForegroundColor = settings.ColorScheme.SecondColor;
        printingImitator.Print(settings.ProgramLanguage.MenuChangeData);
        Console.ForegroundColor = settings.ColorScheme.ErrorColor;
        printingImitator.Print(settings.ProgramLanguage.Quit);
        Console.ForegroundColor = settings.ColorScheme.MainColor;
        
        int itemNumber = UserCommunication.GetMenuItem(8, printingImitator, settings);

        // Действия в зависимости от выбранного пункта меню.
        switch (itemNumber)
        {
            case 1:
                Array.Sort(books,
                    (firstBook, secondBook) => string.CompareOrdinal(firstBook.BookId, secondBook.BookId));
                break;
            case 2:
                Array.Sort(books, 
                    (firstBook, secondBook) => string.CompareOrdinal(firstBook.Title, secondBook.Title));
                break;
            case 3:
                Array.Sort(books, 
                    (firstBook, secondBook) => string.CompareOrdinal(firstBook.Author, secondBook.Author));
                break;
            case 4:
                Array.Sort(books,
                    (firstBook, secondBook) => firstBook.PublicationYear.GetValueOrDefault()
                        .CompareTo(secondBook.PublicationYear.GetValueOrDefault()));
                break;
            case 5:
                Array.Sort(books, 
                    (firstBook, secondBook) => string.CompareOrdinal(firstBook.Genre, secondBook.Genre));
                break;
            case 6:
                Array.Sort(books,
                    (firstBook, secondBook) => firstBook.IsAvailable.GetValueOrDefault()
                        .CompareTo(secondBook.IsAvailable.GetValueOrDefault()));
                break;
            case 7:
                // Получение книги для изменения.
                Book book = ShowListOfTitles(printingImitator, settings, books);
                Console.Clear();

                int item = ShowItemMenu(printingImitator, settings);
                
                printingImitator.Print(settings.ProgramLanguage.InputValue);
                object value = UserCommunication.GetValue(printingImitator, settings, item == 3, item == 5);

                // Редактирование выбранного поля.
                switch (item)
                {
                    case 1:
                        book.Title = (string)value;
                        break;
                    case 2:
                        book.Author = (string)value;
                        break;
                    case 3:
                        book.PublicationYear = (int)value;
                        break;
                    case 4:
                        book.Genre = (string)value;
                        break;
                    case 5:
                        book.IsAvailable = (bool)value;
                        break;
                }
                
                // Запуск события.
                book.RaiseEvent();

                // Если изменилась доступность книги, запись в файл и выдача книги одному из должников.
                if (value is bool && (bool)value)
                {
                    if (book.Borrowers.Count > 0)
                    {
                        Random random = new Random();
                        int index = random.Next(book.Borrowers.Count);
                        Console.WriteLine(index);
                        book.Borrowers.RemoveAt(index);
                        book.IsAvailable = false;
                        book.RaiseEvent();
                    }
                }
                break;
            // Выход из программы.
            case 8:
                Environment.Exit(0);
                break;
        }

        // Выбираем, нужно ли сохранить, возвращаемся к меню.
        ShowSaveOrNotSave(printingImitator, settings, books);
        Thread.Sleep(1000);
        ShowActionMenu(printingImitator, settings, books);
    }

    /// <summary>
    /// Выбор, нужно ли сохранять информацию в файл.
    /// </summary>
    /// <param name="printingImitator">Объект принтера.</param>
    /// <param name="settings">Объект настроек.</param>
    /// <param name="books">Массив книг.</param>
    private static void ShowSaveOrNotSave(PrintingImitator printingImitator, Settings settings, Book[] books)
    {
        Console.ForegroundColor = settings.ColorScheme.MainColor;
        printingImitator.Print(settings.ProgramLanguage.WantToSave);
        Console.ForegroundColor = settings.ColorScheme.FirstColor;
        printingImitator.Print(settings.ProgramLanguage.MenuYes);
        Console.ForegroundColor = settings.ColorScheme.SecondColor;
        printingImitator.Print(settings.ProgramLanguage.MenuNo);
        Console.ForegroundColor = settings.ColorScheme.MainColor;
        
        int itemNumber = UserCommunication.GetMenuItem(2, printingImitator, settings);

        // Сохранение в файл.
        if (itemNumber == 1)
        {
            printingImitator.Print(settings.ProgramLanguage.EnterOutputPath);
            JsonTool.WriteJson(books, UserCommunication.GetFilePath(printingImitator, settings, true),
                printingImitator, settings);
            printingImitator.Print(settings.ProgramLanguage.ChangesSaves);
        } 
    }

    /// <summary>
    /// Выбор поля для редактирования.
    /// </summary>
    /// <param name="printingImitator">Объект принтера.</param>
    /// <param name="settings">Объект настроек.</param>
    /// <returns></returns>
    /// <returns>Выбранный пункт меню.</returns>
    private static int ShowItemMenu(PrintingImitator printingImitator, Settings settings)
    {
        Console.ForegroundColor = settings.ColorScheme.MainColor;
        printingImitator.Print(settings.ProgramLanguage.ChooseItem);
        Console.ForegroundColor = settings.ColorScheme.FirstColor;
        printingImitator.Print(settings.ProgramLanguage.Title);
        Console.ForegroundColor = settings.ColorScheme.SecondColor;
        printingImitator.Print(settings.ProgramLanguage.Author);
        Console.ForegroundColor = settings.ColorScheme.ThirdColor;
        printingImitator.Print(settings.ProgramLanguage.Year);
        Console.ForegroundColor = settings.ColorScheme.FirstColor;
        printingImitator.Print(settings.ProgramLanguage.Genre);
        Console.ForegroundColor = settings.ColorScheme.SecondColor;
        printingImitator.Print(settings.ProgramLanguage.Available);
        Console.ForegroundColor = settings.ColorScheme.MainColor;
        
        return UserCommunication.GetMenuItem(5, printingImitator, settings);
    }

    /// <summary>
    /// Задание настроек.
    /// </summary>
    /// <param name="printingImitator">Объект принтера.</param>
    /// <param name="settings">Объект настроек.</param>
    private static void SetSettings(PrintingImitator printingImitator, Settings settings)
    {   
        ChangeUserName(printingImitator, settings);
        ChangeColorScheme(printingImitator, settings);
        ChangeLanguage(printingImitator, settings);
        ChangeDelay(printingImitator, settings);
        printingImitator.Print(settings.ProgramLanguage.AllChangesAccepted);
        FileTool.CreateFile("settings.dat");
        FileTool.WriteSettingsToFile(settings);
        Environment.Exit(0);
    }

    /// <summary>
    /// Смена имени пользователя.
    /// </summary>
    /// <param name="printingImitator">Объект принтера.</param>
    /// <param name="settings">Объект настроек.</param>
    private static void ChangeUserName(PrintingImitator printingImitator, Settings settings)
    {
        Console.ForegroundColor = settings.ColorScheme.MainColor;
        printingImitator.Print(settings.ProgramLanguage.WantToChangeName);
        Console.ForegroundColor = settings.ColorScheme.FirstColor;
        printingImitator.Print(settings.ProgramLanguage.MenuYes);
        Console.ForegroundColor = settings.ColorScheme.SecondColor;
        printingImitator.Print(settings.ProgramLanguage.MenuNo);
        Console.ForegroundColor = settings.ColorScheme.MainColor;
        
        int itemNumber = UserCommunication.GetMenuItem(2, printingImitator, settings);

        if (itemNumber == 1)
        {
            settings.UserName = UserCommunication.GetUserName(printingImitator, settings);
            printingImitator.Print($"{settings.ProgramLanguage.NameWasChanged}{settings.UserName}");
        } 
    }
    
    /// <summary>
    /// Смена цветовой гаммы.
    /// </summary>
    /// <param name="printingImitator">Объект принтера.</param>
    /// <param name="settings">Объект настроек.</param>
    private static void ChangeColorScheme(PrintingImitator printingImitator, Settings settings)
    {
        Console.ForegroundColor = settings.ColorScheme.MainColor;
        printingImitator.Print(settings.ProgramLanguage.WantToChangeColorScheme);
        Console.ForegroundColor = settings.ColorScheme.FirstColor;
        printingImitator.Print(settings.ProgramLanguage.MenuYes);
        Console.ForegroundColor = settings.ColorScheme.SecondColor;
        printingImitator.Print(settings.ProgramLanguage.MenuNo);
        Console.ForegroundColor = settings.ColorScheme.MainColor;
        
        int itemNumber = UserCommunication.GetMenuItem(2, printingImitator, settings);

        if (itemNumber == 1)
        {
            Console.ForegroundColor = settings.ColorScheme.FirstColor;
            printingImitator.Print(settings.ProgramLanguage.MenuColdTheme);
            Console.ForegroundColor = settings.ColorScheme.SecondColor;
            printingImitator.Print(settings.ProgramLanguage.MenuMainTheme);
            Console.ForegroundColor = settings.ColorScheme.MainColor;
            itemNumber = UserCommunication.GetMenuItem(2, printingImitator, settings);
            if (itemNumber == 1)
            {
                settings.ColorScheme = new ColdColorScheme();
            }
            else
            {
                settings.ColorScheme = new MainColorScheme();
            }
            
            printingImitator.Print(settings.ProgramLanguage.ChangesAccepted);
        } 
    }
    
    /// <summary>
    /// Смена языка приложения.
    /// </summary>
    /// <param name="printingImitator">Объект принтера.</param>
    /// <param name="settings">Объект настроек.</param>
    private static void ChangeLanguage(PrintingImitator printingImitator, Settings settings)
    {
        Console.ForegroundColor = settings.ColorScheme.MainColor;
        printingImitator.Print(settings.ProgramLanguage.WantToChangeLanguage);
        Console.ForegroundColor = settings.ColorScheme.FirstColor;
        printingImitator.Print(settings.ProgramLanguage.MenuYes);
        Console.ForegroundColor = settings.ColorScheme.SecondColor;
        printingImitator.Print(settings.ProgramLanguage.MenuNo);
        Console.ForegroundColor = settings.ColorScheme.MainColor;
        
        int itemNumber = UserCommunication.GetMenuItem(2, printingImitator, settings);

        if (itemNumber == 1)
        {
            Console.ForegroundColor = settings.ColorScheme.FirstColor;
            printingImitator.Print(settings.ProgramLanguage.MenuRussian);
            Console.ForegroundColor = settings.ColorScheme.SecondColor;
            printingImitator.Print(settings.ProgramLanguage.MenuEnglish);
            Console.ForegroundColor = settings.ColorScheme.MainColor;
            itemNumber = UserCommunication.GetMenuItem(2, printingImitator, settings);
            if (itemNumber == 1)
            {
                settings.ProgramLanguage = new MainLanguage();
            }
            else
            {
                settings.ProgramLanguage = new EnglishLanguage();
            }
            
            printingImitator.Print(settings.ProgramLanguage.ChangesAccepted);
        } 
    }
    
    /// <summary>
    /// Смена скорости печати.
    /// </summary>
    /// <param name="printingImitator">Объект принтера.</param>
    /// <param name="settings">Объект настроек.</param>
    private static void ChangeDelay(PrintingImitator printingImitator, Settings settings)
    {
        Console.ForegroundColor = settings.ColorScheme.MainColor;
        printingImitator.Print(settings.ProgramLanguage.WantToChangeDelay);
        Console.ForegroundColor = settings.ColorScheme.FirstColor;
        printingImitator.Print(settings.ProgramLanguage.MenuYes);
        Console.ForegroundColor = settings.ColorScheme.SecondColor;
        printingImitator.Print(settings.ProgramLanguage.MenuNo);
        Console.ForegroundColor = settings.ColorScheme.MainColor;
        
        int itemNumber = UserCommunication.GetMenuItem(2, printingImitator, settings);

        if (itemNumber == 1)
        {
            Console.ForegroundColor = settings.ColorScheme.FirstColor;
            printingImitator.Print(settings.ProgramLanguage.MenuSlow);
            Console.ForegroundColor = settings.ColorScheme.SecondColor;
            printingImitator.Print(settings.ProgramLanguage.MenuMedium);
            Console.ForegroundColor = settings.ColorScheme.ThirdColor;
            printingImitator.Print(settings.ProgramLanguage.MenuFast);
            Console.ForegroundColor = settings.ColorScheme.MainColor;
            itemNumber = UserCommunication.GetMenuItem(3, printingImitator, settings);
            if (itemNumber == 1)
            {
                settings.PrintDelay = 100;
            }
            else if (itemNumber == 2)
            {
                settings.PrintDelay = 30;
            }
            else
            {
                settings.PrintDelay = 15;
            }

            printingImitator.Delay = settings.PrintDelay;
            printingImitator.Print(settings.ProgramLanguage.ChangesAccepted);
        } 
    }
}