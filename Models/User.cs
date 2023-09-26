using System.ComponentModel.DataAnnotations;

namespace OrganizationApplication.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserAddress { get; set; }
        public string UserEmail { get; set; }
        public string PhoneNumber { get; set; }







    }
}
