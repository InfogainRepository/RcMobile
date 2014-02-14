using Newtonsoft.Json;
using RcMobile.Core.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Xml.Linq;

namespace RcMobile.Core.Services
{
    public class RcMobileService : ITask
    {        
        /// <summary>
        /// This method returns Ro List details for the corresponding clientId
        /// </summary>
        /// <param name="authToken"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public IList<RcMobile.Core.Entities.Task> GetTaskList(string authToken, string clientId)
        {
            using (var _client = new HttpClient())
            {
                //Making of the json object to be send with the http request
                var jsonData = "{\"viewName\":\"JobSummaryItemsMobile\",\"filterInfo\":[{\"__type\":\"SimpleFilterDescriptor:http://xyz.com.Data.Entities/2011/09\",\"Name\":\"IncludeOpen\",\"Operation\":0,\"Value\":\"true\"},{\"__type\":\"SimpleFilterDescriptor:http://xyz.com.Data.Entities/2011/09\",\"Name\":\"IncludeClosed\",\"Operation\":0,\"Value\":\"false\"}],\"sortInfo\":[{\"Name\":\"DueOut\",\"Order\":1,\"Count\":0}],\"pageInfo\":{\"Index\":1,\"Size\":20,\"TotalRecords\":0}}";
                HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                //adding headers to the request
                _client.DefaultRequestHeaders.Add("Host", "abc.xyz.com");
                _client.DefaultRequestHeaders.Add("auth_token", authToken);
                _client.DefaultRequestHeaders.Add("client_id", clientId);

                //creating a null list of Tasks
                var result = new List<RcMobile.Core.Entities.Task>(0);

                //Calling post action using Http Client asynchronously
                HttpResponseMessage wcfResponse = _client.PostAsync("https://abc.xyz.com/WebServices/D/JobDataProvider.svc/GetJobSummaryList", content).Result;

                //if the request returns successfully then this code is executed
                if (wcfResponse.StatusCode == HttpStatusCode.OK)
                {
                    HttpContent stream = wcfResponse.Content;

                    //Reading from the stream
                    var data = stream.ReadAsStringAsync();

                    //Deserializing json data
                    XDocument doc = (XDocument)JsonConvert.DeserializeXNode(data.Result);
                    

                    foreach (var item in doc.Descendants("Data"))
                    {
                        result.Add(new RcMobile.Core.Entities.Task
                    {
                        RoNumber = item.Element("RONumber") != null ? item.Element("RONumber").Value : string.Empty,
                        VehicleYear = item.Element("VehicleYear") != null ? item.Element("VehicleYear").Value : string.Empty,
                        VehicleMake = item.Element("VehicleMake") != null ? item.Element("VehicleMake").Value : string.Empty,
                        VehicleLicense = item.Element("VehicleLicense") != null ? item.Element("VehicleLicense").Value : string.Empty,
                        VehicleLicenseState = item.Element("VehicleLicenseState") != null ? item.Element("VehicleLicenseState").Value : string.Empty,
                        VehicleVin = item.Element("VehicleVin") != null ? item.Element("VehicleVin").Value : string.Empty,
                        ClaimNumber = item.Element("ClaimNumber") != null ? item.Element("ClaimNumber").Value : string.Empty,
                        InsuranceCompanyName = item.Element("InsuranceCompanyName") != null ? item.Element("InsuranceCompanyName").Value : string.Empty,
                        DueOut = item.Element("DueOut") == null || string.IsNullOrWhiteSpace(item.Element("DueOut").Value) ? (DateTime?)null : DateTime.Parse(item.Element("DueOut").Value),
                        ArrivedDate = item.Element("ArrivedDate") == null || string.IsNullOrWhiteSpace(item.Element("ArrivedDate").Value) ? (DateTime?)null : DateTime.Parse(item.Element("ArrivedDate").Value),
                        JobId = item.Element("JobId") != null ? item.Element("JobId").Value : string.Empty,
                        OwnerFirstName = item.Element("OwnerFirstName") != null ? item.Element("OwnerFirstName").Value : string.Empty,
                        OwnerLastName = item.Element("OwnerLastName") != null ? item.Element("OwnerLastName").Value : string.Empty,

                        Address = new Address
                        {

                            AddressLine = item.Element("OwnerAddress").Element("AddressLine") != null ? item.Element("OwnerAddress").Element("AddressLine").Value : string.Empty,
                            City = item.Element("OwnerAddress").Element("City") != null ? item.Element("OwnerAddress").Element("City").Value : string.Empty,
                            Province = item.Element("OwnerAddress").Element("Province") != null ? item.Element("OwnerAddress").Element("Province").Value : string.Empty,
                            PostalCode = item.Element("OwnerAddress").Element("OwnerWorkPhone") != null ? item.Element("OwnerAddress").Element("OwnerWorkPhone").Value : string.Empty,
                            OwnerWorkPhone = item.Element("OwnerAddress").Element("OwnerWorkPhone") != null ? item.Element("OwnerAddress").Element("OwnerWorkPhone").Value : string.Empty,
                            OwnerEmail = item.Element("OwnerAddress").Element("OwnerEmail") != null ? item.Element("OwnerAddress").Element("OwnerEmail").Value : string.Empty

                        }
                    });
                    }
                    return result;
                }
                else
                {
                    return result;
                }
            }
        }

        /// <summary>
        /// Returns the details info of a task/job
        /// </summary>
        /// <param name="authToken"></param>
        /// <param name="shopOrgId"></param>
        /// <param name="clientId"></param>
        /// <param name="jobId"></param>
        /// <returns></returns>        
        public InsuredDetails Details(string authToken, string shopOrgId, string clientId, string jobId)
        {
            
            using (var httpClient = new HttpClient())
            {
                //adding headers to the request
                httpClient.DefaultRequestHeaders.Add("Accept", "application/atom+xml");
                httpClient.DefaultRequestHeaders.Add("Accept-Charset", "UTF-8");
                httpClient.DefaultRequestHeaders.Add("DataServiceVersion", "1.0");
                httpClient.DefaultRequestHeaders.Add("MaxDataServiceVersion", "2.0");
                httpClient.DefaultRequestHeaders.Add("auth_token", authToken);
                httpClient.DefaultRequestHeaders.Add("partner_app_id", "3074");
                httpClient.DefaultRequestHeaders.Add("shop_id", clientId);

                //Creating URL for the Http Request with the filters applied as job id and shop id.
                string url = "http://abc.xyz.com/API/1/RepairOrder/CustomerService.svc/InsuredItems(JobId=" + jobId + ",ShopId='" + clientId + "')";
                HttpResponseMessage wcfResponse = null;
                HttpContent stream = null;
                try
                {
                    //Calling get action using Http Client asynchronously
                    wcfResponse = httpClient.GetAsync(url).Result;
                   
                    stream = wcfResponse.Content;
                }
                catch (Exception ex)
                {
                    var a = ex;
                }

                if (wcfResponse.StatusCode == HttpStatusCode.OK)
                {
                    //Reading the stream asynchronously
                    var data1 = stream.ReadAsStringAsync();
                    XDocument doc1 = XDocument.Parse(data1.Result);
                    XNamespace m = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata";
                    XNamespace d = "http://schemas.microsoft.com/ado/2007/08/dataservices";

                    var properties = doc1.Descendants(m + "properties");
                    var obj = new InsuredDetails();
                        obj.NameFirst = properties.FirstOrDefault().Element(d + "NameFirst") != null ? properties.FirstOrDefault().Element(d + "NameFirst").Value : string.Empty;
                        obj.NameLast = properties.FirstOrDefault().Element(d + "NameLast") != null ? properties.FirstOrDefault().Element(d + "NameLast").Value : string.Empty;
                        obj.Address1 = properties.FirstOrDefault().Element(d + "Address1") != null ? properties.FirstOrDefault().Element(d + "Address1").Value : string.Empty;
                        obj.Address2 = properties.FirstOrDefault().Element(d + "Address2") != null ? properties.FirstOrDefault().Element(d + "Address2").Value : string.Empty;
                        obj.PhoneHome = properties.FirstOrDefault().Element(d + "PhoneHome") != null ? properties.FirstOrDefault().Element(d + "PhoneHome").Value : string.Empty;
                        obj.City = properties.FirstOrDefault().Element(d + "City") != null ? properties.FirstOrDefault().Element(d + "City").Value : string.Empty;
                        obj.Email = properties.FirstOrDefault().Element(d + "Email") != null ? properties.FirstOrDefault().Element(d + "Email").Value : string.Empty;
                        obj.ProvinceState = properties.FirstOrDefault().Element(d + "ProvinceState") != null ? properties.FirstOrDefault().Element(d + "ProvinceState").Value : string.Empty;
                        obj.PostalCode = properties.FirstOrDefault().Element(d + "PostalCode") != null ? properties.FirstOrDefault().Element(d + "PostalCode").Value : string.Empty;
                        return obj;
                    }
                else
                {
                    return new InsuredDetails();
                }
            }
        }

        /// <summary>
        /// Returns the address details of claimant
        /// </summary>
        /// <param name="authToken"></param>
        /// <param name="shopOrgId"></param>
        /// <param name="clientId"></param>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public InsuredDetails GetRoClaimantDetails(string authToken, string shopOrgId, string clientId, string jobId)
        {
            
            using (var httpClient = new HttpClient())
            {
                //adding headers to the request
                httpClient.DefaultRequestHeaders.Add("Accept", "application/atom+xml");
                httpClient.DefaultRequestHeaders.Add("Accept-Charset", "UTF-8");
                httpClient.DefaultRequestHeaders.Add("DataServiceVersion", "1.0");
                httpClient.DefaultRequestHeaders.Add("MaxDataServiceVersion", "2.0");
                httpClient.DefaultRequestHeaders.Add("auth_token", authToken);
                httpClient.DefaultRequestHeaders.Add("partner_app_id", "3074");
                httpClient.DefaultRequestHeaders.Add("shop_id", clientId);

                //Creating URL for the Http Request with the filters applied as job id and shop id.
                string url = "http://abc.xyz.com/API/5/RepairOrder/CustomerService.svc/ClaimantList(JobId=" + jobId + ",ShopId='" + clientId + "')";
                HttpResponseMessage wcfResponse = null;
                HttpContent stream = null;
                try
                {
                    //Calling get action using Http Client asynchronously
                    wcfResponse = httpClient.GetAsync(url).Result;
                    stream = wcfResponse.Content;
                }
                catch (Exception ex)
                {
                    var a = ex;
                }
                if (wcfResponse.StatusCode == HttpStatusCode.OK)
                {
                    //Reading the stream asynchronously
                    var data1 = stream.ReadAsStringAsync();
                    XDocument doc1 = XDocument.Parse(data1.Result);
                    XNamespace m = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata";
                    XNamespace d = "http://schemas.microsoft.com/ado/2007/08/dataservices";

                    var properties = doc1.Descendants(m + "properties");
                    var obj = new InsuredDetails();

                    obj.NameFirst = properties.FirstOrDefault().Element(d + "NameFirst") != null ? properties.FirstOrDefault().Element(d + "NameFirst").Value : string.Empty;

                    obj.NameLast = properties.FirstOrDefault().Element(d + "NameLast") != null ? properties.FirstOrDefault().Element(d + "NameLast").Value : string.Empty;
                    obj.Address1 = properties.FirstOrDefault().Element(d + "Address1") != null ? properties.FirstOrDefault().Element(d + "Address1").Value : string.Empty;
                    obj.Address2 = properties.FirstOrDefault().Element(d + "Address2") != null ? properties.FirstOrDefault().Element(d + "Address2").Value : string.Empty;
                    obj.PhoneHome = properties.FirstOrDefault().Element(d + "PhoneHome") != null ? properties.FirstOrDefault().Element(d + "PhoneHome").Value : string.Empty;
                    obj.City = properties.FirstOrDefault().Element(d + "City") != null ? properties.FirstOrDefault().Element(d + "City").Value : string.Empty;
                    obj.Email = properties.FirstOrDefault().Element(d + "Email") != null ? properties.FirstOrDefault().Element(d + "Email").Value : string.Empty;
                    obj.ProvinceState = properties.FirstOrDefault().Element(d + "ProvinceState") != null ? properties.FirstOrDefault().Element(d + "ProvinceState").Value : string.Empty;
                    obj.PostalCode = properties.FirstOrDefault().Element(d + "PostalCode") != null ? properties.FirstOrDefault().Element(d + "PostalCode").Value : string.Empty;
                    return obj;
                }
                else
                {
                    return new InsuredDetails();
                }
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
