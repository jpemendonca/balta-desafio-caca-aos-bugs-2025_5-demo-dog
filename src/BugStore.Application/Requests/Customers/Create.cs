using System.ComponentModel.DataAnnotations;

namespace BugStore.Application.Requests.Customers;

public class Create
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
}