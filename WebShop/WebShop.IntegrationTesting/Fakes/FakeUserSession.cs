using WebShop.Core.Users;

namespace WebShop.IntegrationTesting.Fakes
{
    public class FakeUserSession : IUserSession
    {
        public UserModel User { get; set; }

        public FakeUserSession()
        {
            if (User == null)
            {
                User = new UserModel();
            }
        }
    }
}