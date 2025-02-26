using Microsoft.AspNetCore.Mvc;
using PizzaShop.Domain.Models;
using PizzaShop.Domain.ViewModels;

namespace PIzzaShop.Service.Interfaces;

public interface IUserListService
{
    public  Task<List<UserListViewModel>> getUsers();
    public  Task<List<UserListViewModel>> getSearchedUserListDetails(string searchTerm);

    public Task<EditUserViewModel> getUserDataFromUserId(int id,int userLoggedInId);

    public List<Role> getRoles ();

    public Task<JsonResult> editUserDataFromUserId(int userLoggedInId,EditUserViewModel userViewModel);

    public  Task<string> getUserNameFromId(int id);

    public Task<string> getImageUrlFromId(int id);

    public Task<int> getRoleIdFromId(int id);

    public Task<JsonResult> AddUser(int userLoggedInId, AddUserViewModel addUserViewModel);
    
     public  Task<JsonResult> DeleteUser(int userId);
}
