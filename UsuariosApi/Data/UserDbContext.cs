using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using UsuariosApi.Models;

namespace UsuariosApi.Data
{
    public class UserDbContext : IdentityDbContext<CustomIdentityUser, IdentityRole<int>, int>
    {

        public UserDbContext(DbContextOptions<UserDbContext> opt) : base(opt)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            CustomIdentityUser admin = new CustomIdentityUser
            {
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                //Carimbar o momento da criação desse usuário
                //Gui gera um identificador único
                SecurityStamp = Guid.NewGuid().ToString(),
                Id = 99999
            };

            PasswordHasher<CustomIdentityUser> hasher = new PasswordHasher<CustomIdentityUser>();

            //HashPassword = gera um hash a partir de uma senha
            admin.PasswordHash = hasher.HashPassword(admin, "Admin123!");

            //build desse usuario como uma entidade
            builder.Entity<CustomIdentityUser>().HasData(admin);

            //Criação da role admin
            builder.Entity<IdentityRole<int>>().HasData(new IdentityRole<int>()
            {
                Id = 99999,
                Name = "admin",
                NormalizedName = "Admin"
            });

            //Atribuindo a role ao usuário admin
            builder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int> { RoleId = 99999, UserId = 99999 });

            builder.Entity<IdentityRole<int>>().HasData(new IdentityRole<int>()
            {
                Id = 99997,
                Name = "regular",
                NormalizedName = "REGULAR"
            });
        }
    }
}