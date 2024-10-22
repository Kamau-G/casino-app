using Microsoft.EntityFrameworkCore;
using CasinoApp.Models;
using Microsoft.AspNetCore.Identity;
namespace CasinoApp.Data
{
    public class AppDbInit
    {
        public static async Task Seed(AppDbContext db, IServiceProvider provider)
        {
            var userManager = provider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
            var password = "Password123!";

            if(roleManager.FindByNameAsync("HisMajesty").Result == null && roleManager.FindByNameAsync("Player").Result == null)
            {
                var roleNames = new List<string> { "Player", "HisMajesty" };
                //roleNames.ForEach(async r => { await roleManager.CreateAsync(new IdentityRole(r)); });
                foreach (var roles in roleNames) {
                    await roleManager.CreateAsync(new IdentityRole(roles));
                }
            }
            var r = roleManager.FindByNameAsync("Admin").Result;
            if (roleManager.FindByNameAsync("Admin").Result == null)
            {
                string username = "Admin";
                string adminpassword = "AdminPass123!";
                string roleName = "Admin";
                // Add these rolls to app if time.
                
                // if role doesn't exist, create it
                await roleManager.CreateAsync(new IdentityRole(roleName));

                // if username doesn't exist, create it and add to role
                if (await userManager.FindByNameAsync(username) == null)
                {
                    AppUser user = new AppUser { Name=username,UserName = username };
                    var result = await userManager.CreateAsync(user, adminpassword);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, roleName);
                    }
                }
            }
            if (!db.DiceGameScores.Any() || !db.Messages.Any()) { 
                Random _rand = new Random();
                var kambucha = new AppUser { Name = "Kambucha",UserName= "Kambucha" };
                var avocado = new AppUser { Name = "Avocado", UserName = "Avocado" };
                await userManager.CreateAsync(kambucha, password);
                await userManager.CreateAsync(avocado, password);
                kambucha = await userManager.FindByNameAsync("Kambucha");
                avocado = await userManager.FindByNameAsync("Avocado");
                List<CardGameScore> scores = new List<CardGameScore> {
                        new CardGameScore { Score = _rand.Next(200),Player = kambucha},
                        new CardGameScore { Score = _rand.Next(200),Player = avocado},
                        new CardGameScore { Score = _rand.Next(200),Player = kambucha},
                        new CardGameScore { Score = _rand.Next(200),Player = avocado},
                        new CardGameScore { Score = _rand.Next(200),Player = kambucha},
                    };
                foreach (var score in scores)
                {

                await db.CardGameScores.AddAsync(score);
                }
                await db.SaveChangesAsync();
                List<DiceGameScore> DGScores = new List<DiceGameScore> {
                        new DiceGameScore { Score = _rand.Next(200),Player = avocado},
                        new DiceGameScore { Score = _rand.Next(200),Player = kambucha},
                        new DiceGameScore { Score = _rand.Next(200),Player = avocado},
                        new DiceGameScore { Score = _rand.Next(200),Player = kambucha},
                        new DiceGameScore { Score = _rand.Next(200),Player = avocado},
                };
                foreach (var score in DGScores)
                {

                    await db.DiceGameScores.AddAsync(score);
                }
                await db.SaveChangesAsync();
                var m = new Message
                {
                    Rcpnt = kambucha,
                    Sndr = avocado,
                    Text = "This is the first",
                }; 
                var m2 = new Message
                {
                    Rcpnt = avocado,
                    Sndr = kambucha,
                    Text = "This is the second",
                }; 
                await db.Messages.AddAsync(m);
                await db.Messages.AddAsync(m2); 
                await db.SaveChangesAsync();
            }

        }


    }
}
