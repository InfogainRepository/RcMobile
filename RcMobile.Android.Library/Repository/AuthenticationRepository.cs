
using RcMobile.Android.Library.ViewModels;
using RcMobile.Core.Services;

namespace RcMobile.Android.Library.Repository
{
    public class AuthenticationRepository
    {
        private readonly IAuthenticate _repos;

        public AuthenticationRepository()
        {
            if (_repos == null)
            {
                _repos = new RcAuthenticationService();
            }
        }

        /// <summary>
        /// This method authenticate the user by using passed details.
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserCredential IsAuthenticated(string clientId, string userName, string password)
        {
            //Ignore the Server Certificate to call the Web service to login
            System.Net.ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) => { return true; };

            //Calling the web service located in the core PCL library through interface
            var credencial = _repos.IsAuthenticated(clientId, userName, password);
            return new UserCredential
            {
                AuthToken = credencial.AuthToken,
                ShopId = credencial.ShopId,
                ShopOrgId = credencial.ShopOrgId
            };
        }
    }
}