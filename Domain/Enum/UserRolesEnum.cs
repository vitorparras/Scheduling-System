namespace Domain.Enum
{
    [Flags]
    public enum UserRolesEnum
    {
        None = 0,
        Admin,
        Employee,
        Client
    }
}
