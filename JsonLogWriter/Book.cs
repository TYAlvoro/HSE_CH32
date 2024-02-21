using System.Text.Json.Serialization;
namespace JsonLogWriter;

public class Book
{   
    [JsonPropertyName("bookId")]
    public string BookId { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("author")]
    public string Author { get; set; }

    [JsonPropertyName("publicationYear")]
    public int PublicationYear { get; set; }

    [JsonPropertyName("genre")]
    public string Genre { get; set; }

    [JsonPropertyName("isAvailable")]
    public bool IsAvailable { get; set; }
    
    [JsonPropertyName("borrowers")]
    public Borrower[] Borrowers { get; set; }

    public event EventHandler<LibraryEventArgs> Updated; 
    
    public Book() {}

    public Book(string bookId, string title, string author, int publicationYear,
        string genre, bool isAvailable, Borrower[] borrowers)
    {
        BookId = bookId;
        Title = title;
        Author = author;
        PublicationYear = publicationYear;
        Genre = genre;
        IsAvailable = isAvailable;
        Borrowers = borrowers;
    }

    public void RaiseEvent()
    {
        TimeSpan timeWithoutMilliseconds = new TimeSpan(DateTime.Now.TimeOfDay.Hours, 
            DateTime.Now.TimeOfDay.Minutes, DateTime.Now.TimeOfDay.Seconds);
        LibraryEventArgs eventArgs = new LibraryEventArgs(DateTime.Today, timeWithoutMilliseconds);
        OnUpdateAcquired(this, eventArgs);
    }

    public void OnUpdateAcquired(object sender, LibraryEventArgs args) => Updated?.Invoke(sender, args);

    public void ToJson()
    {
        throw new NotImplementedException();
    }

    private void CheckNull(){}
}