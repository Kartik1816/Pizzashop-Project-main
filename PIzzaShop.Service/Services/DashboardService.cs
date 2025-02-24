
// using PizzaShop.Domain.DBContext;
// using PIzzaShop.Service.Interfaces;

// namespace PIzzaShop.Service.Services;

// public class DashboardService : IDashboardService
// {
//     private readonly PizzaShopDbContext _pizzaShopDbContext;
//     private readonly IGenerateJwt _generateJwt;
//     public DashboardService(PizzaShopDbContext pizzaShopDbContext,IGenerateJwt generateJwt)
//     {
//       _pizzaShopDbContext=pizzaShopDbContext;
//         _generateJwt=generateJwt;
//     }

//     public string getUsernameFromId(int userId)
//     {
//         var username=_pizzaShopDbContext.Users.FirstOrDefault(u=>u.Id==userId).Username;
//         return username;
//     }
//     public string getImageUrlFromId(int userId)
//     {
//         return _pizzaShopDbContext.Users.FirstOrDefault(u=>u.Id==userId).ProfileImage;
//     }
// }
