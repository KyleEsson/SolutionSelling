using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolutionSelling.Areas.Identity.Data;
using SolutionSelling.Models;

namespace SolutionSelling.Areas.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<SolutionSellingUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

internal class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<SolutionSellingUser>
{

    // CONFIGURE THE USER TABLE
    public void Configure(EntityTypeBuilder<SolutionSellingUser> builder)
    {
        builder.Property(u => u.FirstName).HasMaxLength(50);
        builder.Property(u => u.LastName).HasMaxLength(50);
    }
}

    // POPULATE THE DATABASE WITH DATA DURING MIGRATION
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);



        // HASHING THE PASSWORD
        var hasher = new PasswordHasher<SolutionSellingUser>();

        //_________________________________________________________ADMIN ROLE__________________________________________________________________

        // SEEDING THE ADMIN ROLE
        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "2c5e174e-3b0e-446f-86af-483d56fd7210", Name = "Administrator", NormalizedName = "ADMINISTRATOR".ToUpper() });

        // SEEDING THE ADMIN USER
        modelBuilder.Entity<SolutionSellingUser>().HasData(
            new SolutionSellingUser
            {
                Id = "8e445865-a24d-4543-a6c6-9443d048cdb9", 
                FirstName = "Admin",
                LastName = "Admin",
                UserName = "admin@administrator.com",
                NormalizedUserName = "ADMIN@ADMINISTRATOR.COM",
                Email = "admin@administrator.com",
                NormalizedEmail = "ADMIN@ADMINISTRATOR.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Admin")
            }
        );


        // SEEDING THE RELATIONSHIP BETWEEN OUR USER AND ROLE 
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
            }
        );

        //_________________________________________________________USER ROLES__________________________________________________________________

        // SEEDING THE USER ROLE
        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "3c5e174e-3b0e-446f-86af-483d56fd7210", Name = "User", NormalizedName = "USER".ToUpper() });

        // SEEDING THE USER
        modelBuilder.Entity<SolutionSellingUser>().HasData(
           new SolutionSellingUser
           {
               Id = "7e445865-a24d-4543-a6c6-9443d048cdb9", 
               FirstName = "Kyle",
               LastName = "Esson",
               UserName = "kyle@test.com",
               NormalizedUserName = "KYLE@TEST.COM",
               Email = "kyle@test.com",
               NormalizedEmail = "KYLE@TEST.COM",
               EmailConfirmed = true,
               PasswordHash = hasher.HashPassword(null, "Kyle")
           }
        );

        //SEEDING THE RELATIONSHIP BETWEEN OUR USER AND ROLE
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = "3c5e174e-3b0e-446f-86af-483d56fd7210",
                UserId = "7e445865-a24d-4543-a6c6-9443d048cdb9"
            }
        );

        // SEEDING THE USER
        modelBuilder.Entity<SolutionSellingUser>().HasData(
           new SolutionSellingUser
           {
               Id = "6e445865-a24d-4543-a6c6-9443d048cdb9", 
               FirstName = "Bec",
               LastName = "Detourbet",
               UserName = "bec@test.com",
               NormalizedUserName = "BEC@TEST.COM",
               Email = "bec@test.com",
               NormalizedEmail = "BEC@TEST.COM",
               EmailConfirmed = true,
               PasswordHash = hasher.HashPassword(null, "Bec")
           }
        );

        // SEEEDING THE RELATIONSHIP BETWEEN OUR USER AND ROLE
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = "3c5e174e-3b0e-446f-86af-483d56fd7210",
                UserId = "6e445865-a24d-4543-a6c6-9443d048cdb9"
            }
        );

        // SEEDING THE USER
        modelBuilder.Entity<SolutionSellingUser>().HasData(
           new SolutionSellingUser
           {
               Id = "5e445865-a24d-4543-a6c6-9443d048cdb9", 
               FirstName = "John",
               LastName = "Smith",
               UserName = "john@test.com",
               NormalizedUserName = "JOHN@TEST.COM",
               Email = "john@test.com",
               NormalizedEmail = "JOHN@TEST.COM",
               EmailConfirmed = true,
               PasswordHash = hasher.HashPassword(null, "John")
           }
        );

        // SEEEDING THE RELATIONSHIP BETWEEN OUR USER AND ROLE
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = "3c5e174e-3b0e-446f-86af-483d56fd7210",
                UserId = "5e445865-a24d-4543-a6c6-9443d048cdb9"
            }
        );

        modelBuilder.ApplyConfiguration(new ApplicationUserEntityConfiguration());


    }
}


