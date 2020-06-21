using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.models
{
    public class Vehicle
    {
        // public int Id { get; set; }

        // [Required]
        // [Range(0, 9999.99)]
        // public double ? Price { get; set; }

        // [Required]
        // public string Title { get; set; }

        // [Required]
        // public string color { get; set; }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Model { get; set; }
        public string Engine { get; set; }
        public string Color { get; set; }
        public string Company { get; set; }
        public DateTime DatePosted { get; set; }
        public bool IsHotandNew { get; set; }
        public bool IsFeatured { get; set; }
        public string Location { get; set; }
        public string Condition { get; set; }

        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public ICollection<Image> Images { get; set; }
    }
}