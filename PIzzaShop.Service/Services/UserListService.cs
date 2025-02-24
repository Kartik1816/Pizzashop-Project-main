// using Demo.Models;

// namespace Demo;

// public class UserListService : IUserListService
// {
//     private readonly PizzashopContext _pizzashopContext;

//     public UserListService(PizzashopContext pizzashopContext)
//     {
//         _pizzashopContext=pizzashopContext;
//     }
//     public List<UserListViewModel> getUsers()
//     {
//         var usersWithRoles = _pizzashopContext.Users
//         .Join(
//             _pizzashopContext.Roles,
//             user => user.RoleId,
//             role => role.Id,
//             (user, role) => new UserListViewModel
//             {
//                 Username = user.Username,
//                 Email = user.Email,
//                 Phone = user.Phone,
//                 RoleName = role.Name,
//                 Status = user.Status
//             })
//         .ToList();

//     return usersWithRoles;
//     }

 

// }
