using RcMobile.Core.Entities;

using System;
using System.Collections.Generic;


namespace RcMobile.Core.Services
{
    /// <summary>
    /// An interface for the all task related services.
    /// </summary>
    public interface ITask : IDisposable
    {        
        /// <summary>
        /// This method returns Ro List details for the corresponding clientId
        /// </summary>
        /// <param name="authToken"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        IList<RcMobile.Core.Entities.Task> GetTaskList(string authToken, string clientId);
 
        /// <summary>
        /// Returns the details info of a task
        /// </summary>
        /// <param name="authToken"></param>
        /// <param name="shopOrgId"></param>
        /// <param name="clientId"></param>
        /// <param name="jobId"></param>
        /// <returns></returns>
        InsuredDetails Details(string authToken, string shopOrgId, string clientId, string jobId);
        
        /// <summary>
        /// Returns the address details of claimant
        /// </summary>
        /// <param name="authToken"></param>
        /// <param name="shopOrgId"></param>
        /// <param name="clientId"></param>
        /// <param name="jobId"></param>
        /// <returns></returns>
        InsuredDetails GetRoClaimantDetails(string authToken, string shopOrgId, string clientId, string jobId);
    }
}
