
using Microsoft.AspNetCore.Mvc;
using PizzaShop.Domain.DBContext;
using PizzaShop.Domain.Models;
using PizzaShop.Domain.ViewModels;
using PizzaShop.Repository.Interfaces;


namespace PIzzaShop.Service.Interfaces;

public class UserListService : IUserListService
{
    private readonly IUserListRepository _userListRepository;

    private readonly IUserRepository _userRepository;

    public UserListService(IUserListRepository userListRepository,IUserRepository userRepository)
    {
       _userListRepository=userListRepository;
       _userRepository=userRepository;
    }
    public async Task<List<UserListViewModel>> getUsers()
    {
        var usersWithRoles = await _userListRepository.getUserWithRole();

        return  usersWithRoles;
    }

    public async Task<List<UserListViewModel>> getSearchedUserListDetails(string searchTerm)
    {
        var usersWithRoles = await _userListRepository.getUserWithRole();

         if (!string.IsNullOrEmpty(searchTerm))
        {
            searchTerm = searchTerm.ToLower();
            usersWithRoles = usersWithRoles.Where(u =>
                u.Username.ToLower().Contains(searchTerm) ||
                u.Email.ToLower().Contains(searchTerm) ||
                u.Phone.Contains(searchTerm) ||
                u.RoleName.ToLower().Contains(searchTerm)
            ).ToList();
        }

        return usersWithRoles.ToList();
    }

     public async Task<EditUserViewModel> getUserDataFromUserId(int id,int userLoggedInId)
     {
            var usernameLoggedIn = await _userRepository.getUserNameFromUserId(userLoggedInId);
            var profileImageURLLoggedIn = await _userRepository.getImageUrlFromUserId(userLoggedInId);
            var user = await _userRepository.getUserById(id);   
            var roleIdLoggedIn =await _userRepository.getRoleIdFromUserId(userLoggedInId);     
            var userViewModel = new EditUserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.Phone,
                Address = user.Address,
                ZipCode = user.ZipCode,
                Status = (bool)user.Status,
                Username = usernameLoggedIn,
                RoleId = roleIdLoggedIn,
                ProfileImageURL = profileImageURLLoggedIn,
                RoleIdRequestedUser = user.RoleId,
                CountryId = (int)user.CountryId,
                StateId = (int)user.StateId,
                CityId = (int)user.CityId,
                UsernameRequestedUSer = user.Username,
            };
            return  userViewModel;
     }

         public List<Role> getRoles ()
         {
            return _userRepository.getRoleList();
         }
     public async Task<JsonResult> editUserDataFromUserId(int userLoggedInId,EditUserViewModel userViewModel)
     {
        return await _userListRepository.saveEditedUser(userLoggedInId,userViewModel);
     }
      public async Task<string> getUserNameFromId(int id)
     {
        return await _userRepository.getUserNameFromUserId(id);
     }

    public async Task<string> getImageUrlFromId(int id)
    {
        return await _userRepository.getImageUrlFromUserId(id);
    }

    public async Task<int> getRoleIdFromId(int id)
    {
        return await _userRepository.getRoleIdFromUserId(id);
    }

    public async Task<JsonResult> AddUser(int userLoggedInId, AddUserViewModel addUserViewModel)
    {
        return await _userListRepository.addUser(userLoggedInId,addUserViewModel);
    }

    public async Task<JsonResult> DeleteUser(int userId)
        {
            return await _userListRepository.deleteUser(userId);
        }
}
