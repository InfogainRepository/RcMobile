using RcMobile.Core.Entities;

using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Xml.Linq;

namespace RcMobile.Core.Services
{
    public class RcAuthenticationService : IAuthenticate
    {
        /// <summary>
        /// This method authenticate the user by using passed details.
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public LoginResponse IsAuthenticated(string clientId, string userName, string password)
        {           

            var loginResponse = RemoteLoginService("https://xyz.database.com", clientId, userName, password);

            const string AuthenticationServiceCertificate = "MIIG4zCCBcsaddsfdfgdfhfghgu6ytiuyiyuhgmhjkhjkjhKLJKHUHKJHGHJgGHGHguy78798t3k0C6ID/97fDgsxKhVRnvioo2FSiv1YnMqccLfb6VefWO3A+435sdfds01i4W5CvKn69vRqOaB721mhlCCW0XXzAdHz75rEvnBKF6rPaAaYBMy1SBJCA+DLoJP2t9kFzmvzZ6G7C7T6YuB8QUet/6rduQUjs0/Ry/mL6E1i9atg8Ezx3Fl2j+YUBTaxWXPOlhJX7ViiIBUl6EhQfs8PU/5hTmvnoEhFicRLS5FNz8NGc9ivi9AgMBAAGjggPAMIIDvDAbBg0rBgEEAYKKQgEBAgEBAQH/BAcwBYADMS4wMBkGDSsGAQQBgopCAQECAQMECDAGgARQcm9kMCsGDCsGAQSCikIBAQIBBAQbMBmAF0VQRFNlcnZpY2VQcm92aWRlclRva2VuMIIDGwYNKwYBBAGCikICAQEBAQEB/wSCAwUwggMBgIIC/Tw/eG1sIHZlcnNpb249IjEuMCIgZW5jb2Rpbmc9InV0Zi04Ij8+PFVzZXJJbmZvVHlwZSB4bWxuczp4c2k9Imh0dHA6Ly93d3cudzMub3JnLzIwMDEvWE1MU2NoZW1hLWluc3RhbmNlIiB4bWxuczp4c2Q9Imh0dHA6Ly93d3cudzMub3JnLzIwMDEvWE1MU2NoZW1hIiB4bWxucz0iaHR0cDovL3d3dy5taXRjaGVsbC5jb20vY29tbW9uL3R5cGVzIj48T3JnSUQ+MzU2NjUwODwvT3JnSUQ+PE9yZ0NvZGU+QlM8L09yZ0NvZGU+PFVzZXJJRD44NjkwMDI8L1VzZXJJRD48Rmlyc3ROYW1lPk1JVENIRUxMPC9GaXJzdE5hbWU+PExhc3ROYW1lPkFQSSBJTlRFUk5BTCBQQVJUTkVSPC9MYXN0TmFtZT48RW1haWw+eXVhbi5jaGVuZ0BtaXRjaGVsbC5jb208L0VtYWlsPjxVc2VySGllcj48SGllck5vZGUgTGV2ZWw9IkNPTVBBTlkiIENvZGU9IkJTIiBJRD0iMzA3IiBOYW1lPSIiPjxIaWVyTm9kZSBMZXZlbD0iT0ZGSUNFIiBDb2RlPSJEMTZLIiBJRD0iMzU2NjUwNyIgTmFtZT0iTUlUQ0hFTEwgQVBJIElOVEVSTkFMIFBBUlRORVIiPjxIaWVyTm9kZSBMZXZlbD0iVVNFUiIgQ29kZT0iODY5MDAyIiBJRD0iMzU2NjUwOCIgTmFtZT0iTUlUQ0hFTEwgQVBJIElOVEVSTkFMIFBBUlRORVIiIC8+PC9IaWVyTm9kZT48L0hpZXJOb2RlPjwvVXNlckhpZXI+PEFwcENvZGU+Q1JTQVBQPC9BcHBDb2RlPjxBcHBDb2RlPk1JQ1JDRjwvQXBwQ29kZT48QXBwQ29kZT5SQ0FQUDwvQXBwQ29kZT48U3RhZmZUeXBlPkVTVElNQVRPUjwvU3RhZmZUeXBlPjwvVXNlckluZm9UeXBlPjAZBg0rBgEEAYKKQgEBAgECBAgwBoAEdHJ1ZTAbBgwrBgEEgopCAgEBAQIECzAJgAczNTY2NTA4MA0GCSqGSIb3DQEBBAUAA4IBAQBz4f+JDNdQI15Zktmp/UO9f56pW/AP+IIFv65S8PAyUlQAyycmd2Y0WuGaAphX4SChSm3/uwMfNhmiIR8XzDbm16+xVi1Z4zEYLu6SproVnXi2dVX8BW/kYM2aQ7FZfbfdoBTQOBOSYs2P49JjVmqhm8L8Rr08Ouii1Zl8BkE2hHnh5BIOzIwVbCxSFTUt1sio648y3nwMeSXRbQj07PGdIOxqzaiuSmNHjir+7YKREJgNY1IV4oxeBHrOf2HjznYALp5m0T1BFtcAcxAv0f0oMeHrGimrkgHf/mjvTjGoCyPcPAdYqB0ROOKMbMT7mK70FuMV3s4TsgQgIDR+VLOn";
            const string trackingId = "114454-06534-1246-0000-006590890000";

            loginResponse.AuthToken = GetAuthToken(AuthenticationServiceCertificate, loginResponse.ShopOrgId, trackingId);
            return loginResponse;
        }
                
        /// <summary>
        /// This method takes the credentials and returns ShopId and ShopOrgId
        /// </summary>
        /// <param name="providerAddress"></param>
        /// <param name="accessCode"></param>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        private LoginResponse RemoteLoginService(string providerAddress, string accessCode, string userName, string passWord)
        {
            using (var httpClient = new HttpClient())
            {

                //The soap message with accesscode, username and password
                string soapMessage = String.Format(@"<?xml version=""1.0"" encoding=""utf-8""?>
                                              <SOAP-ENV:Envelope xmlns:xsd=""http://www.w3.org/2001/XMLSchema""
                                                  xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                                                  xmlns:SOAP-ENC=""http://schemas.xmlsoap.org/soap/encoding/""
                                                  xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"">
                                                  <SOAP-ENV:Body>
                                                      <Login xmlns=""http://xyz.Services/2010/09"">
                                                          <accessCode>{0}</accessCode>
                                                          <userName>{1}</userName>
                                                          <password>{2}</password>
                                                      </Login>
                                                  </SOAP-ENV:Body>
                                              </SOAP-ENV:Envelope>", accessCode, userName, passWord);

                //Adding the soap message in content of http request
                HttpContent content = new StringContent(soapMessage, Encoding.UTF8, "text/xml");

                // The SOAPAction header indicates which method you would like to invoke
                httpClient.DefaultRequestHeaders.Add("SOAPAction", "http://xyz.Services/LoginService/Login");

                //Calling post action using Http Client asynchronously
                HttpResponseMessage wcfResponse = httpClient.PostAsync(string.Format("{0}/Services/InternalApps/LoginService.svc", providerAddress), content).Result;

                //if the request returns successfully then this code is executed
                if (wcfResponse.StatusCode == HttpStatusCode.OK)
                {
                    HttpContent stream = wcfResponse.Content;
                    var data = stream.ReadAsStringAsync();
                    XDocument doc = XDocument.Parse(data.Result);
                    XNamespace a = "http://schemas.datacontract.org/xyz/212/Data.Entities.InternalApps";
                    return new LoginResponse
                    {
                        ShopId = doc.Descendants(a + "ShopId").First().Value,
                        ShopOrgId = doc.Descendants(a + "ShopOrgId").First().Value
                    };
                }
                else
                {
                    return new LoginResponse();
                }
            }
        }


        /// <summary>
        /// This method takes certificate , shopid and shoporgid from previous method and returns authentication token for further services.
        /// </summary>
        /// <param name="certificate"></param>
        /// <param name="shopOrgId"></param>
        /// <param name="trackingId"></param>
        /// <returns></returns>
        private string GetAuthToken(string certificate, string shopOrgId, string trackingId)
        {
            using (var httpClient = new HttpClient())
            {
                string soapMessage = String.Format(@"<?xml version=""1.0"" encoding=""utf-8""?>
                                                <SOAP-ENV:Envelope xmlns:xsd=""http://www.w3.org/2001/XMLSchema""
                                                    xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                                                    xmlns:SOAP-ENC=""http://schemas.xmlsoap.org/soap/encoding/""
                                                    xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"">
                                                    <SOAP-ENV:Body>
                                                        <GenerateTokenMessage xmlns=""http://xyz.security.authentication.server.ServiceContracts/20032/07/29"">
                                                            <Request xmlns:d4p1=""http://xyz.security.authentication.server.DataContracts/2009/04/29"">
                                                                <d4p1:Certificate>{0}</d4p1:Certificate>
                                                                <d4p1:ServiceProvider>{1}</d4p1:ServiceProvider>
                                                                <d4p1:TrackingId>{2}</d4p1:TrackingId>
                                                            </Request>
                                                            <Response xmlns:d4p1=""http://xyz.security.authentication.server.DataContracts/2009/07/29"" xmlns:i=""http://www.w3.org/2001/XMLSchema-instance"">
                                                                <d4p1:Token i:nil=""true""></d4p1:Token>
                                                                <d4p1:AuthenticationResult xmlns:d5p1=""http://schemas.datacontract.org/2004/07/xyz.communications"" i:nil=""true""></d4p1:AuthenticationResult>
                                                            </Response>
                                                        </GenerateTokenMessage>
                                                    </SOAP-ENV:Body>
                                                </SOAP-ENV:Envelope>", certificate, shopOrgId, trackingId);

                //Adding the soap message in content of http request
                HttpContent content = new StringContent(soapMessage, Encoding.UTF8, "text/xml");

                // The SOAPAction header indicates which method you would like to invoke
                httpClient.DefaultRequestHeaders.Add("SOAPAction", "http://xyz.security.authentication.server.serviceContracts/2009/07/29/IAuthenticationService/GenerateToken");

                //Calling post action using Http Client asynchronously
                HttpResponseMessage wcfResponse = httpClient.PostAsync("https://xyz.com/authenticationService/authenticationService.svc", content).Result;

                //if the request returns successfully then this code is executed
                if (wcfResponse.StatusCode == HttpStatusCode.OK)
                {
                    HttpContent stream = wcfResponse.Content;

                    //Reading from the stream
                    var response = stream.ReadAsStringAsync();

                    //Parsing xml document
                    XDocument doc = XDocument.Parse(response.Result);
                    XNamespace a = "http://xyz.security.authentication.server.DataContracts/2009/07/29";
                    string auth_token = doc.Descendants(a + "Token").First().Value;
                    return auth_token;
                }
                return null;
            }
        }

        public void Dispose()
        {
            var dis = this as IDisposable;
            if (dis != null)
            {
                dis.Dispose();
            }
        }
    }
}
