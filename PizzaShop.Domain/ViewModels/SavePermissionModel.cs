namespace PizzaShop.Domain.ViewModels;

public class SavePermissionModel
{
    public int RoleId {get;set;}

    public int PermissionId {get;set;}

    public string PermissionType {get;set;}

    public bool PermissionValue {get;set;}
}
