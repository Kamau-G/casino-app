using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CasinoApp.Models;

public class AdminVM {
    public IEnumerable<AppUser>?Users { get; set; }
    public IEnumerable<AppUser>?Admins { get; set; }
    public IEnumerable<IdentityRole>?Roles { get; set; }
}