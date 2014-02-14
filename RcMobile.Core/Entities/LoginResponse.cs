
namespace RcMobile.Core.Entities
{
    /// <summary>
    /// Holds the info of login credentials to pass other methods.
    /// </summary>
    public class LoginResponse
    {
        public string AuthToken { get; set; }

        public string ShopId { get; set; }

        public string ShopOrgId { get; set; }
    }
}
