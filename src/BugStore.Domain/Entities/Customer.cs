namespace BugStore.Domain.Entities;

public class Customer
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime BirthDate { get; set; }
    public int Age
    {
        get
        {
            var today = DateTime.Today;
            var age = today.Year - BirthDate.Year;
            
            if (BirthDate.Date > today.AddYears(-age)) 
                age--;
            
            return age;
        }
    }
}