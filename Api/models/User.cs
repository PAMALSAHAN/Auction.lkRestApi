using System.Collections.Generic;

namespace Api.models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string ImageURL { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}