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

}