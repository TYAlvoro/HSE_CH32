namespace ToolLibrary;

/// <summary>
/// Класс для удобной работы с файлами.
/// </summary>
public static class FileTool
{
    /// <summary>
    /// Считывание настроек.
    /// </summary>
    /// <returns>Объект настроек.</returns>
    public static Settings OpenFileAndSetSettings()
    {
        Settings settings = new Settings();
        
        // Создание файла при первом запуске программы.
        if (!File.Exists("settings.dat"))
        {
            CreateFile("settings.dat");
            WriteSettingsToFile(settings);
            return settings;
        }
        
        // Считывание файла с настройками.
        using (StreamReader streamReader = new StreamReader("settings.dat"))
        {
            settings.PrintDelay = int.Parse(streamReader.ReadLine()!);
            settings.ProgramLanguage =
                streamReader.ReadLine() == "Russian" ? new MainLanguage() : new EnglishLanguage();
            settings.ColorScheme =
                streamReader.ReadLine() == "Warm" ? new MainColorScheme() : new ColdColorScheme();
            settings.UserName = streamReader.ReadLine()!;
        }

        return settings;
    }

    /// <summary>
    /// Безопасное создание файла.
    /// </summary>
    /// <param name="filePath">Путь до файла.</param>
    public static void CreateFile(string filePath)
    {
        using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
        {
        }
    }

    /// <summary>
    /// Запись настроек.
    /// </summary>
    /// <returns>Объект настроек.</returns>
    public static void WriteSettingsToFile(Settings settings)
    {
        using (StreamWriter streamWriter = new StreamWriter("settings.dat"))
        {
            streamWriter.WriteLine(settings.PrintDelay);
            streamWriter.WriteLine(settings.ProgramLanguage);
            streamWriter.WriteLine(settings.ColorScheme);
            streamWriter.WriteLine(settings.UserName);
        }
    }
    
    /// <summary>
    /// Проверка корректности пути до файла.
    /// </summary>
    /// <param name="filePath">Путь до файла.</param>
    /// <param name="settings">Объект настроек.</param>
    /// <exception cref="ArgumentNullException">Пустой путь.</exception>
    /// <exception cref="FileNotFoundException">Не найден файл.</exception>
    public static void CheckFilePath(string? filePath, Settings settings)
    {
        // Если путь пустой, то выбрасывается исключение.
        if (string.IsNullOrEmpty(filePath) || string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentNullException(nameof(filePath),settings.ProgramLanguage.FilePathIsEmpty);
        }
        // Если указанный файл не существует, то выбрасывается исключение.
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException(settings.ProgramLanguage.FileNotFound);
        }
    }
    
    /// <summary>
    /// Проверка корректности пути до директории.
    /// </summary>
    /// <param name="directoryPath">Путь до директории.</param>
    /// <param name="settings">Объект настроек.</param>
    /// <exception cref="ArgumentNullException">Пустой путь.</exception>
    /// <exception cref="DirectoryNotFoundException">Не найдена директория.</exception>
    public static void CheckDirectoryPath(string? directoryPath, Settings settings)
    {
        // Если путь пустой, то выбрасывается исключение.
        if (string.IsNullOrEmpty(directoryPath) || string.IsNullOrWhiteSpace(directoryPath))
        {
            throw new ArgumentNullException(nameof(directoryPath), settings.ProgramLanguage.DirectoryPathIsEmpty);
        }

        // Если указанная директория не существует, то выбрасывается исключение.
        if (!Directory.Exists(directoryPath))
        {
            throw new DirectoryNotFoundException(settings.ProgramLanguage.DirectoryNotFound);
        }
    }

}