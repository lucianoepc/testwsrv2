//using System.ComponentModel.DataAnnotations.Schema;

namespace PoC.TestWServ2.Common.Entities;

public class Person
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    //[Column(TypeName = "timestamp with time zone")]
    public DateTime DateOfBirth { get; set; }
    public string PlaceOfBirth { get; set; } = string.Empty;
    public int IdentityDocumentTypeId { get; set; }
    
    // Navigation property
    public IdentityDocumentType? IdentityDocumentType { get; set; }
    
    public int Age 
    { 
        get
        {
            return DateTime.Today.Year - DateOfBirth.Year - 
                (DateTime.Today.DayOfYear < DateOfBirth.DayOfYear ? 1 : 0);
        }
    }
}