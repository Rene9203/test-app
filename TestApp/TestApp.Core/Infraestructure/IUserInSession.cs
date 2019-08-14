namespace TestApp.Core.Infraestructure
{
    public interface IUserInSession
    {
        string UserId { get; set; }
        string UserName { get; set; }
    }
}
