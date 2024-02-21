namespace ToolLibrary;

public class PrintingImitator
{
    private int _delay;

    public int Delay
    {
        set => _delay = value;
    }

    public PrintingImitator(){}

    public PrintingImitator(int delay) => Delay = delay;

    public void Print(string valueToPrint)
    {
        foreach (char symbol in valueToPrint)
        {
            Console.Write(symbol);
            Thread.Sleep(_delay);
        }

        Console.WriteLine(); 
    }
}