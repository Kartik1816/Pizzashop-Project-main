
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
    public async Task<UserListViewModel> getUsers(int userId,int PageIndex,int PageSize)
    {
        var usernameLoggedIn=await _userRepository.getUserNameFromUserId(userId);
        var url=await _userRepository.getImageUrlFromUserId(userId);
        List<User> usersWithRoles = await _userListRepository.getUserWithRole(null);
        var totalusers=usersWithRoles.Count();
        var totalPages=(int)Math.Ceiling((double)totalusers/PageSize);
        usersWithRoles=usersWithRoles.Take(PageSize).ToList();
        UserListViewModel userListViewModel=new UserListViewModel
        {
            users=usersWithRoles,
            UserName=usernameLoggedIn,
            ProfileImageURL=url,
            PageIndex=PageIndex,
            PageSize=PageSize,
            TotalUsers=totalusers,
            TotalPages=totalPages
        };

        return  userListViewModel;
    }

    public async Task<List<User>> getTotalUsersInTable(string searchTerm)
    {
         return await _userListRepository.getUserWithRole(searchTerm);
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
                UserName=usernameLoggedIn,
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
