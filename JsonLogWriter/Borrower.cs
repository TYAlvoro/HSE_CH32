using System.Text.Json;
using System.Text.Json.Serialization;

namespace JsonLogWriter;

/// <summary>
/// Класс, хранящий информацию о должнике.
/// </summary>
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