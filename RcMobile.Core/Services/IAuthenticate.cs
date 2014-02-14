using RcMobile.Core.Entities;

using System;

namespace RcMobile.Core.Services
{
    /// <summary>
    /// An interface for the Authentication service methods.
    /// </summary>
    public interface IAuthenticate : IDisposable
    {
        /// <summary>
        /// This method authenticate the user by using passed details.
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        LoginResponse IsAuthenticated(string clientId, string userName, string password);
    }
}
