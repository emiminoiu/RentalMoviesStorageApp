namespace RentalMoviesApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
          Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'3290fe81-d34e-4302-9129-7e992e6d71c7', N'admin2@yahoo.com', 0, N'AJceKOEbeS+Rg/5EYbq5GUZzQShfsADWywNQBN7HStNgAOWYq/NFbOyUyyD/3W705w==', N'fc39e041-c254-48c0-8817-446203b1ac44', NULL, 0, 0, NULL, 1, 0, N'admin2@yahoo.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'3293c1b8-f89e-42fd-a2e8-d0f53b3da947', N'admin@yahoo.com', 0, N'AP7juqZhFxB9TgjcTnETAX3Du50ve5y8rra7Qextsf6sDdEfUESUtkr+wlu/UKIkCA==', N'3d416713-63c3-49b5-8727-d2feec3e98da', NULL, 0, 0, NULL, 1, 0, N'admin@yahoo.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'cb0cbdc3-e7ab-4664-a979-5216659bd5be', N'minoiu_emi@yahoo.com', 0, N'AEWmbGywN0fQtfnQWlTrLVC4JW8Si0okKVlXgrpKKSjcK+yKhM4bHOE888i1VUfdyQ==', N'9d6b6367-476e-4979-b593-00fa16d0f509', NULL, 0, 0, NULL, 1, 0, N'minoiu_emi@yahoo.com')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'8d4ab42a-13c4-491d-a4b6-56339003f8ff', N'CanManageMovies')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'3290fe81-d34e-4302-9129-7e992e6d71c7', N'8d4ab42a-13c4-491d-a4b6-56339003f8ff')

");
        }
        
        public override void Down()
        {
           
        }
    }
}
