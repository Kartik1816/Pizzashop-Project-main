namespace PizzaShop.Domain.ViewModels;

public class UserListViewModel
{
    public int UserId { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? RoleName { get; set; }
    public bool? Status { get; set; }

}
