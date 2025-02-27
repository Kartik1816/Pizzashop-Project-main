using PizzaShop.Domain.Models;

namespace PizzaShop.Domain.ViewModels;

public class UserListViewModel
{
    public List<User> users;
    public string? UserName { get; set; }
    public string? ProfileImageURL { get; set; }

    public int PageIndex { get; set; }

    public int TotalPages { get; set; }
    
    public int TotalUsers { get; set; }

    public int PageSize { get; set; }

}
