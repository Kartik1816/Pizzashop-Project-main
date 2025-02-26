namespace PIzzaShop.Service.Interfaces;

public interface IDashboardService
{
    public Task<string> getUsernameFromId(int userId);
    public Task<string> getImageUrlFromId(int userId);
}
