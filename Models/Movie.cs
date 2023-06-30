using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Models;


//Movie model containing getter/setters for the values held by the database
public class Movie
{
    //Primary Key
    public int Id { get; set; }

    //Journal Entry Date
    [DataType(DataType.Date)]
    [DisplayName("Entry Date")]
    public DateTime EntryDate { get; set; }

    //Movie Title
    [StringLength(60, MinimumLength = 3)]
    [Required]
    [RegularExpression(@"^[A-Za-z0-9\s]+$", ErrorMessage = "Invalid Movie Name")]
    [DisplayName("Movie Title")]
    public string? MovieName { get; set; } = string.Empty;

    //Genre
    [StringLength(60, MinimumLength = 1)]
    [Required]
    [RegularExpression(@"^[A-Za-z-]+$", ErrorMessage = "Invalid Genre")] 
    public string? Genre { get; set; }

    //Release Year
    [Range(1888, int.MaxValue, ErrorMessage = "Invalid Release Year")]
    [CurrentYearMaxValue(ErrorMessage = "Release Year cannot be in the future")]
    public int ReleaseYear { get; set; }

    //Journal Entry
    [DisplayName("Journal Entry")]
    [RegularExpression(@"^[a-zA-Z0-9 ,.!?'"":;()\[\]]*$", ErrorMessage = "Invalid characters in Journal Entry.")]
    public string? JournalEntry { get; set; }
}


//Validation code to check the current year max value for the ReleaseYear variable validation
public class CurrentYearMaxValueAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value is null || !(value is int)) return false;

        var compareValue = (int)value;
        if (compareValue > DateTime.Now.Year) return false;

        return true;
    }
}