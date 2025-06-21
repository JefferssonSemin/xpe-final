namespace Delivery.Comunication.RequestModel.User;

public class RequestLoginJson(string username, string password)
{
    public string Username { get; } = username;
    public string Password { get; } = password;
}