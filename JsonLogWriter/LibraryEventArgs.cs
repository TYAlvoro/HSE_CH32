namespace JsonLogWriter;

public class LibraryEventArgs: EventArgs
{
    public DateTime UpdateDate { get; set; }
    public TimeSpan UpdateTime { get; set; }
    
    public LibraryEventArgs() {}

    public LibraryEventArgs(DateTime updateDate, TimeSpan updateTime)
    {
        UpdateDate = updateDate;
        UpdateTime = updateTime;
    }
    
}