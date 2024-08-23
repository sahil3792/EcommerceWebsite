using System.ComponentModel.DataAnnotations;

namespace E_commerce_webApp.Models
{
    public class Cart
    {
        [Key]
        public int Pid { get; set; }
        public string? Pname { get; set; }
        public string? Pcat { get; set; }
        public string? Picture { get; set; }
        public double Price { get; set; }
        public string User {  get; set; }
    }
}
