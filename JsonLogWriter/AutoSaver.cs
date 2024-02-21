namespace JsonLogWriter;

public static class AutoSaver
{
    private static TimeSpan _previousEventTime = TimeSpan.MinValue;
    public static void DoSomething(object sender, LibraryEventArgs args)
    {
        if (_previousEventTime != TimeSpan.MinValue)
        {
            TimeSpan timeDifference = args.UpdateTime - _previousEventTime;
            if (timeDifference.TotalSeconds <= 15)
            {
                //TODO: здесь будут происходить запись и т.д.
            }
        }

        _previousEventTime = args.UpdateTime;
    }  
}