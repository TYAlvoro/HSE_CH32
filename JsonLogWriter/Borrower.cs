using System.Text.Json.Serialization;

namespace JsonLogWriter;

public class Borrower
{
    [JsonPropertyName("borrowerId")]
    public string BorrowerId { get; set; }
    
    [JsonPropertyName("borrowerName")]
    public string BorrowerName { get; set; }
    
    [JsonPropertyName("borrowDate")]
    public string BorrowDate { get; set; }
    
    [JsonPropertyName("dueDate")]
    public string DueDate { get; set; }
    
    public event EventHandler<LibraryEventArgs> Updated; 
    
    public Borrower() {}

    public Borrower(string borrowerId, string borrowerName, string borrowDate, string dueDate)
    {
        BorrowerId = borrowerId;
        BorrowerName = borrowerName;
        BorrowDate = borrowDate;
        DueDate = dueDate;
    }
    
    public void RaiseEvent()
    {
        LibraryEventArgs eventArgs = new LibraryEventArgs(DateTime.Today, DateTime.Now.TimeOfDay);
        OnUpdateAcquired(this, eventArgs);
    }
    
    public void OnUpdateAcquired(object sender, LibraryEventArgs args) => Updated?.Invoke(sender, args);
    
    public void ToJson()
    {
        throw new NotImplementedException();
    }
}