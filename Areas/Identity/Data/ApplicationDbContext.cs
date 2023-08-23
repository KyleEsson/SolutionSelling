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

    //protected override void OnModelCreating(ModelBuilder builder)
    //{
        //base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        //builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    //}
internal class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<SolutionSellingUser>
{
    public void Configure(EntityTypeBuilder<SolutionSellingUser> builder)
    {
        builder.Property(u => u.FirstName).HasMaxLength(50);
        builder.Property(u => u.LastName).HasMaxLength(50);
    }
}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);



        //a hasher to hash the password before seeding the user to the db
        var hasher = new PasswordHasher<SolutionSellingUser>();

        //_________________________________________________________ADMIN ROLE__________________________________________________________________

        //Seeding a  'Administrator' role 
        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "2c5e174e-3b0e-446f-86af-483d56fd7210", Name = "Administrator", NormalizedName = "ADMINISTRATOR".ToUpper() });

        //Seeding the admin user to AspNetUsers table
        modelBuilder.Entity<SolutionSellingUser>().HasData(
            new SolutionSellingUser
            {
                Id = "8e445865-a24d-4543-a6c6-9443d048cdb9", // primary key
                FirstName = "Giga",
                LastName = "Chad",
                UserName = "admin@administrator.com",
                NormalizedUserName = "ADMIN@ADMINISTRATOR.COM",
                Email = "admin@administrator.com",
                NormalizedEmail = "ADMIN@ADMINISTRATOR.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Admin")
            }
        );


        //Seeding the relation between our user and role to AspNetUserRoles table
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
            }
        );

        //_________________________________________________________USER ROLES__________________________________________________________________

        //Seeding a 'User' role
        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "3c5e174e-3b0e-446f-86af-483d56fd7210", Name = "User", NormalizedName = "USER".ToUpper() });

        modelBuilder.Entity<SolutionSellingUser>().HasData(
           new SolutionSellingUser
           {
               Id = "7e445865-a24d-4543-a6c6-9443d048cdb9", // primary key
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

        //Seeding the relation between our user and role to AspNetUserRoles table
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = "3c5e174e-3b0e-446f-86af-483d56fd7210",
                UserId = "7e445865-a24d-4543-a6c6-9443d048cdb9"
            }
        );

        modelBuilder.Entity<SolutionSellingUser>().HasData(
           new SolutionSellingUser
           {
               Id = "6e445865-a24d-4543-a6c6-9443d048cdb9", // primary key
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

        //Seeding the relation between our user and role to AspNetUserRoles table
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = "3c5e174e-3b0e-446f-86af-483d56fd7210",
                UserId = "6e445865-a24d-4543-a6c6-9443d048cdb9"
            }
        );

        modelBuilder.Entity<SolutionSellingUser>().HasData(
           new SolutionSellingUser
           {
               Id = "5e445865-a24d-4543-a6c6-9443d048cdb9", // primary key
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

        //Seeding the relation between our user and role to AspNetUserRoles table
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


