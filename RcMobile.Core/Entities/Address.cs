
namespace RcMobile.Core.Entities
{
    /// <summary>
    /// A generic class to hold the address details.
    /// </summary>
    public class Address
    {
        public string AddressLine { get; set; }
        
        public string City { get; set; }
        
        public string Province { get; set; }
        
        public string PostalCode { get; set; }
        
        public string OwnerWorkPhone { get; set; }
        
        public string OwnerEmail { get; set; }
    }
}
