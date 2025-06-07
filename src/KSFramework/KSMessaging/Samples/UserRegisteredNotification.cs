using KSFramework.KSMessaging.Abstraction;

namespace KSFramework.KSMessaging.Samples;

/// <summary>
/// Notification that is triggered when a user registers.
/// </summary>
public class UserRegisteredNotification : INotification
{
    public string Username { get; }

    public UserRegisteredNotification(string username)
    {
        Username = username;
    }
}