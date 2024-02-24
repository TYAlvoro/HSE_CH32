namespace ToolLibrary;

/// <summary>
/// Класс для красивой печати в консоль.
/// </summary>
public class PrintingImitator
{
    private int _delay;

    public int Delay
    {
        set => _delay = value;
    }

    public PrintingImitator(){}

    public PrintingImitator(int delay) => Delay = delay;

    /// <summary>
    /// Печать посимвольно в консоль.
    /// </summary>
    /// <param name="valueToPrint">Строка для печати.</param>
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