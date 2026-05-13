namespace Faradars.Shared.Constants;

public static class RoleConstants
{
    public static readonly Role Manager = new(1, "Manager");
    public static readonly Role Admin = new(2, "Admin");
    public static readonly Role User = new(3, "User");

    public record Role(int Id, string Name);
}