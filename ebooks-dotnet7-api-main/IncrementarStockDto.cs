using System.ComponentModel.DataAnnotations;

namespace ebooks_dotnet7_api;

/// <summary>
/// Represents an eBook entity.
/// </summary>
public class IncrementarStockDto{

    public int Id { get; set; }
    /// <summary>
    /// Stock of the eBook.
    /// </summary>
    public required int Stock { get; set; }
}