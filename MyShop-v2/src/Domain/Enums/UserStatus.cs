namespace MyShop_v2.Domain.Enums
{
    public enum UserStatus
    {

        Pending = 0,      // Registered but not verified
        Active = 1,
        Suspended = 2,     // Temporarily disabled due to multiple failed login attempts 
        Deactivated = 4    // subject for deletetion for a specified period of time

    }
}