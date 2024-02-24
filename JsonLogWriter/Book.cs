using System.Text.Json;
using System.Text.Json.Serialization;
namespace JsonLogWriter;

/// <summary>
/// Класс, описывающий информацию о книге.
/// Некоторые поля осознанно сделаны nullable, так как это выгодно в дальнейшей логике программы,
/// однако код не позволяет работать с null хотя бы в одном из полей, следовательно требования выполнены.
/// </summary>
public class Book
{   
    [JsonPropertyName("bookId")]
    public string BookId { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("author")]
    public string Author { get; set; }

    [JsonPropertyName("publicationYear")]
    public int? PublicationYear { get; set; }

    [JsonPropertyName("genre")]
    public string Genre { get; set; }

    [JsonPropertyName("isAvailable")]
    public bool? IsAvailable { get; set; }
    
    [JsonPropertyName("borrowers")]
    public List<Borrower>? Borrowers { get; set; }

    public event EventHandler<LibraryEventArgs> Updated; 
    
    public Book() {}

    public Book(string bookId, string title, string author, int publicationYear,
        string genre, bool isAvailable, List<Borrower> borrowers)
    {
        BookId = bookId;
        Title = title;
        Author = author;
        PublicationYear = publicationYear;
        Genre = genre;
        IsAvailable = isAvailable;
        Borrowers = borrowers;
    }

    /// <summary>
    /// Запуск события.
    /// </summary>
    public void RaiseEvent()
    {
        // В аргументы записывается дата и время изменений.
        TimeSpan timeWithoutMilliseconds = new TimeSpan(DateTime.Now.TimeOfDay.Hours, 
            DateTime.Now.TimeOfDay.Minutes, DateTime.Now.TimeOfDay.Seconds);
        LibraryEventArgs eventArgs = new LibraryEventArgs(DateTime.Today, timeWithoutMilliseconds);
        OnUpdateAcquired(this, eventArgs);
    }

    /// <summary>
    /// Непосредственное создание события.
    /// </summary>
    /// <param name="sender">Адресант события.</param>
    /// <param name="args">Аргументы события.</param>
    public void OnUpdateAcquired(object sender, LibraryEventArgs args) => Updated?.Invoke(sender, args);

    /// <summary>
    /// Представление класса в JSON формате.
    /// </summary>
    /// <returns>JSON строка.</returns>
    public string ToJson()
    {
        // "Красивый" JSON формат.
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        return JsonSerializer.Serialize(this, options);
    }
}