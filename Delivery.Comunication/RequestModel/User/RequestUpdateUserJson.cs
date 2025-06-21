namespace Delivery.Comunication.RequestModel.User;

public class RequestUpdateUserJson(string userName, string role)
{
    public string UserName { get;  } = userName;
    public string Role { get;  } = role;
}