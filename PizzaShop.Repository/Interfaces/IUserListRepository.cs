using Microsoft.AspNetCore.Mvc;
using PizzaShop.Domain.ViewModels;

namespace PizzaShop.Repository.Interfaces;

public interface IUserListRepository
{
    public Task<List<UserListViewModel>> getUserWithRole();
    public  Task<JsonResult> saveEditedUser(int userLoggedInId, EditUserViewModel userViewModel);
}
