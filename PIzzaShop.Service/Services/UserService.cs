using PizzaShop.Repository.Interfaces;


using PIzzaShop.Service.Interfaces;
using PizzaShop.Domain.Models;

namespace PIzzaShop.Service.Services;


    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {

            _userRepository=userRepository;
        }

        public async Task<User> validateUserAsync(string email, string password)
        {
             return await _userRepository.getUserByEmailAndPasswordAsync(email,password);
            //return await Task.Run(() => _context.Users.Any(u => u.Email == email && u.Password == password));
        }

         public async Task<string> getRoleName(int id)
         {
            return await _userRepository.getRoleNameFromRoleId(id);
         }

        
    }

