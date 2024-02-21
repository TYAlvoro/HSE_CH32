namespace ToolLibrary;

public class MainColorScheme
{
    public virtual ConsoleColor MainColor => ConsoleColor.DarkYellow;
    public virtual ConsoleColor QuestionColor => ConsoleColor.DarkBlue;
    public virtual ConsoleColor OkColor => ConsoleColor.DarkGreen;
    public virtual ConsoleColor ErrorColor => ConsoleColor.DarkRed;
    public virtual ConsoleColor FirstColor => ConsoleColor.DarkCyan;
    public virtual ConsoleColor SecondColor => ConsoleColor.DarkGray;
    public virtual ConsoleColor ThirdColor => ConsoleColor.DarkMagenta;

    public MainColorScheme() {}
    
    public override string ToString()
    {
        return "Warm";
    }
}