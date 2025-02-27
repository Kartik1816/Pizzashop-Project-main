using Microsoft.AspNetCore.Mvc;
using PizzaShop.Domain.Models;
using PizzaShop.Domain.ViewModels;

namespace PizzaShop.Repository.Interfaces;

public interface IUserListRepository
{
    public Task<List<User>> getUserWithRole(string searchTerm);
    public  Task<JsonResult> saveEditedUser(int userLoggedInId, EditUserViewModel userViewModel);
    public  Task<JsonResult> addUser(int userLoggedInId, AddUserViewModel addUserViewModel);

    public Task<JsonResult> deleteUser(int userId);
}
