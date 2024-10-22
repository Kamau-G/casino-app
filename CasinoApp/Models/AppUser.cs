using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CasinoApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CasinoApp.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; } = "";
        public int? Coins { get; set; } = 1000;
        [NotMapped]
        public IList<string>? RoleNames { get; set; }
    }
}
