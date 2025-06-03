using KSFramework.Messaging.Abstraction;

namespace KSFramework.Messaging.Samples;

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