namespace ToolLibrary;

public class Settings
{
    public int PrintDelay { get; set; } = 30;
    public MainLanguage ProgramLanguage { get; set; } = new MainLanguage();
    public MainColorScheme ColorScheme { get; set; } = new MainColorScheme();

    public string UserName { get; set; } = Environment.UserName;
    
    public Settings() {}

    public Settings(int delay, MainLanguage programLanguage, MainColorScheme colorScheme, string userName)
    {
        PrintDelay = delay;
        ProgramLanguage = programLanguage;
        ColorScheme = colorScheme;
        UserName = userName;
    }
}