using System.ComponentModel.DataAnnotations;

namespace ebooks_dotnet7_api;

/// <summary>
/// Represents an eBook entity.
/// </summary>
public class ActualizarDto
{
    /// <summary>
    /// Unique identifier for the eBook.
    /// </summary>
    public int Id { get;set;}

    /// <summary>
    /// Title of the eBook.
    /// </summary>
    [StringLength(256, MinimumLength = 3)]
    public required string Title { get; set; }

    /// <summary>
    /// Author of the eBook.
    /// </summary>
    [StringLength(256, MinimumLength = 3)]
    public required string Author { get; set; }

    /// <summary>
    /// Genre of the eBook.
    /// </summary>
    [StringLength(256, MinimumLength = 3)]
    public required string Genre { get; set; }

    /// <summary>
    /// Format of the eBook.
    /// </summary>
    [StringLength(256, MinimumLength = 3)]
    public required string Format { get; set; }

    /// <summary>
    /// Price of the eBook.
    /// </summary>
    [Range(1, int.MaxValue)]
    public required int Price { get; set; }
}
