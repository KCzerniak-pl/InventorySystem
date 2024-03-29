﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities.User
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Email { get; set; } = default!;

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; } = default!;

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; } = default!;

        [Required]
        public string PasswordHash { get; set; } = default!;

        [Required]
        [Column(TypeName = "datetimeoffset(0)")]
        public DateTimeOffset DateTimeCreated { get; set; }

        [Required]
        public int RoleId { get; set; }

        // Relationships.
        public Role Role { get; set; } = default!;
    }
}
