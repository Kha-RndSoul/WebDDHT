using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebDDHT.Models
{
    public class Banner
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public bool Status { get; set; } // true = active, false = inactive

        // Constructors

        public Banner()
        {
        }

        // Constructor to create new banner
        public Banner(string title, string imageUrl, bool status)
        {
            Title = title;
            ImageUrl = imageUrl;
            Status = status;
        }

        public Banner(int id, string title, string imageUrl, bool status)
        {
            Id = id;
            Title = title;
            ImageUrl = imageUrl;
            Status = status;
        }

        // Check if banner is active
        public bool IsActive()
        {
            return Status;
        }

        public override string ToString()
        {
            return $"Banner{{Id={Id}, Title='{Title}', ImageUrl='{ImageUrl}', Status={Status}}}";
        }
    }
}