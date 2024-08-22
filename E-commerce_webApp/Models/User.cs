using Microsoft.AspNetCore.Mvc;

namespace E_commerce_webApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }

        [Remote(action: "CheckExistingEmailID", controller:"Auth")]
        public string Email { get; set; }
            
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
