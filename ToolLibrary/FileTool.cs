namespace ToolLibrary;

public static class FileTool
{
    public static Settings OpenFileAndSetSettings()
    {
        Settings settings = new Settings();
        
        if (!File.Exists("settings.dat"))
        {
            CreateSettingsFile();
            WriteSettingsToFile(settings);
            return settings;
        }
        
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

    public static void CreateSettingsFile()
    {
        using (FileStream fileStream = new FileStream("settings.dat", FileMode.Create))
        {
        }
    }

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