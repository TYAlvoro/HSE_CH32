namespace ToolLibrary;

public class ColdColorScheme: MainColorScheme
{
    public override ConsoleColor MainColor => ConsoleColor.Yellow;
    public override ConsoleColor QuestionColor => ConsoleColor.Blue;
    public override ConsoleColor OkColor => ConsoleColor.Green;
    public override ConsoleColor ErrorColor => ConsoleColor.Red;
    public override ConsoleColor FirstColor => ConsoleColor.Cyan;
    public override ConsoleColor SecondColor => ConsoleColor.Gray;
    public override ConsoleColor ThirdColor => ConsoleColor.Magenta;

    public ColdColorScheme() {}

    public override string ToString()
    {
        return "Cold";
    }
}