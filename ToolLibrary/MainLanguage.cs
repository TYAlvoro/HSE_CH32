namespace ToolLibrary;
public class MainLanguage
{
    public virtual string Greeting => "Привет, ";
    public virtual string ProgramDestiny => "Здесь Вы можете следить за доступностью книг в библиотеке.";
    public virtual string WantToConfigure => "Не желаете ли настроить программу перед использованием?";
    public virtual string WhichNumRequired => "Должно быть введено число от 1 до ";
    public virtual string MenuYes => "1. Да";
    public virtual string MenuNo => "2. Нет";
    public virtual string WantToChangeName =>
        "Хотите ли Вы указать, как к Вам обращаться? Пример: Темный Лорд, Обер-лейтенант Брауманн и т.д.";
    public virtual string UserNameIsIncorrect => "Имя пользователя не может быть пустым!";
    public virtual string InputUserName => "Введите имя пользователя...";
    public virtual string NameWasChanged => "Имя пользователя изменено на: ";
    public virtual string WantToChangeColorScheme => "Хотите ли Вы выбрать цветовую схему приложения?";
    public virtual string ChangesAccepted => "Изменения приняты!";
    public virtual string MenuColdTheme => "1. Холодная тема";
    public virtual string MenuMainTheme => "2. Теплая тема";
    public virtual string WantToChangeLanguage => "Хотите ли Вы выбрать язык приложения?";
    public string MenuRussian => "1. Русский язык";
    public string MenuEnglish => "2. English";
    public virtual string WantToChangeDelay => "Хотите ли Вы выбрать скорость печати в приложении?";
    public virtual string MenuSlow => "1. Медленно";
    public virtual string MenuMedium => "2. Нормально";
    public virtual string MenuFast => "3. Быстро";
    public virtual string AllChangesAccepted => "Все изменения приняты, для применения перезагрузите приложение!";
    public virtual string LetsStart =>
        $", приступим!{Environment.NewLine}Вам предоставляется удобный софт для работы с данными библиотеки. " +
        $"Здесь вы можете фильтровать список книг по различным критериям,{Environment.NewLine}изменять информацию о " +
        $"книгах и оповещать людей в листе ожидания о возможности взять ту или иную книгу.";
    public virtual string EnterInputFilePath =>
        "Для корректной работы программы, введите путь к файлу с данными о библиотеке..."; 
    public virtual string EnterOutputFilePath =>
        "Для корректной работы программы, введите путь к папке для выходных данных. Файл для вывода создастся автоматически...";
    public virtual string FilePathIsEmpty => "Полученный путь к файлу пуст!";
    public virtual string FileNotFound => "Файл по указанному пути не существует!";
    public virtual string DirectoryPathIsEmpty => "Полученный путь к директории пуст!";
    public virtual string DirectoryNotFound => "Директория по указанному пути не существует!";
    
    public override string ToString()
    {
        return "Russian";
    }
}