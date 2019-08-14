using TestApp.Core.Infraestructure;

namespace TestApp.Core.SharedKernel
{
    public class UserInSession : IUserInSession
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
