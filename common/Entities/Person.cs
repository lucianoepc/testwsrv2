namespace PoC.TestWServ2.Common.Entities;

public class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string PlaceOfBirth { get; set; } = string.Empty;
    public int Age 
    { 
        get
        {
            return DateTime.Today.Year - DateOfBirth.Year - 
                (DateTime.Today.DayOfYear < DateOfBirth.DayOfYear ? 1 : 0);
        }
    }
}