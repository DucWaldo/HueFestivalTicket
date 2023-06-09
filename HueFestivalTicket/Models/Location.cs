﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalTicket.Models
{
    [Table("Location")]
    public class Location
    {
        [Key]
        public Guid IdLocation { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? Decription { get; set; }
        public string? Address { get; set; }
        public string? ImageUrl { get; set; }
        public Guid IdTypeLocation { get; set; }

        [ForeignKey("IdTypeLocation")]
        public TypeLocation? TypeLocation { get; set; }
    }
}
