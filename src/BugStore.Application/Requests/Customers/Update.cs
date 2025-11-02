using System.ComponentModel.DataAnnotations;

namespace BugStore.Application.Requests.Customers;

public class Update
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}