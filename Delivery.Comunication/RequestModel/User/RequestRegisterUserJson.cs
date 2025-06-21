namespace Delivery.Comunication.RequestModel.User;

public class RequestRegisterUserJson(string password)
{
    public string UserName { get; set; }  = string.Empty;
    public string Password { get;  } = password;
    public string Role { get; set; }  = string.Empty;
}