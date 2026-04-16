using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryProject.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone {  get; set; }
        public bool IsActive { get; set; }

        public int RoleID { get; set; }
        public Role UserRoles { get; set; }

        public Client? client { get; set; }
        public Driver? driver { get; set; }


    }
}
