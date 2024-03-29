﻿using DsK.ITSM.Security.EntityFramework.Models;
using DsK.ITSM.Security.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

internal class Program
{
    private static void Main(string[] args)
    {
        var options = new DbContextOptions<DsKitsmContext>();
        
        //var db = new DsKitsmContext(new DbContextOptionsBuilder<DsKitsmContext>().UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SecurityTablesTest;Trusted_Connection=True;Trust Server Certificate=true").Options);
        var db = new DsKitsmContext(new DbContextOptionsBuilder<DsKitsmContext>().UseSqlServer("Server=.;Database=DsKITSM;Trusted_Connection=True;Trust Server Certificate=true").Options);
        db.Database.Migrate(); //CREATES DATABASE IF IT DOESNT EXISTS
        db.Database.EnsureCreated(); //CREATES TABLES IF IT DOESNT EXISTS


        AuthenticationProvider localAuthProvider = CreateLocalAuthProvider(db);
        Permission adminPermission = CreateAdminPermission(db);
        Role adminRole = CreateAdminRole(db);
        CreateUserRole(db);
        AddAdminPermissionToAdminRole(db, adminPermission, adminRole);
        User adminUser = CreateAdminUser(db);
        AddAuthenticationProviderToAdminUser(db, localAuthProvider, adminUser);
        AddAdminRoleToAdminUser(db, adminRole, adminUser);
        
        CreateAdminUserPassword(db, adminUser);

        var permissionList = DsK.ITSM.Security.Shared.Constants.Access.GetRegisteredPermissions();
        foreach (var permission in permissionList)
        {
            db.Permissions.Add(new Permission() { PermissionName = permission, PermissionDescription = "" });
        }
        db.SaveChanges();

    }
    private static void CreateAdminUserPassword(DsKitsmContext db, User adminUser)
    {
        var ramdomSalt = SecurityHelpers.RandomizeSalt;

        var userPassword = new UserPassword()
        {
            UserId = adminUser.Id,
            HashedPassword = SecurityHelpers.HashPasword("admin123", ramdomSalt),
            Salt = Convert.ToHexString(ramdomSalt),
            DateCreated = DateTime.Now
        };
        db.UserPasswords.Add(userPassword);
        db.SaveChanges();
    }
    private static AuthenticationProvider CreateLocalAuthProvider(DsKitsmContext db)
    {
        var authProvider = new AuthenticationProvider()
        {
            AuthenticationProviderName = "Local",
            AuthenticationProviderType= "Local",
            Domain="",
            Username="",
            Password=""
        };
        db.AuthenticationProviders.Add(authProvider);
        db.SaveChanges();
        return authProvider;
    }
    private static void AddAdminRoleToAdminUser(DsKitsmContext db, Role adminRole, User adminUser)
    {
        var adminUserRole = new UserRole() { Role = adminRole, User = adminUser };
        db.UserRoles.Add(adminUserRole);
        db.SaveChanges();
    }    
    private static void AddAuthenticationProviderToAdminUser(DsKitsmContext db, AuthenticationProvider localAuthProvider, User adminUser)
    {
        var adminAuthenticationProvider = new UserAuthenticationProvider() { 
            AuthenticationProvider = localAuthProvider, 
            User = adminUser, Username = 
            adminUser.Username 
        };
        db.UserAuthenticationProviders.Add(adminAuthenticationProvider);
        db.SaveChanges();
    }
    private static User CreateAdminUser(DsKitsmContext db)
    {
        var adminUser = new User()
        {
            Username = "admin",
            Name = "admin",
            Email = "admin@admin.com",
            EmailConfirmed = true
        };

        db.Users.Add(adminUser);
        db.SaveChanges();
        return adminUser;
    }
    private static void AddAdminPermissionToAdminRole(DsKitsmContext db, Permission adminPermission, Role adminRole)
    {
        var adminRolePermission = new RolePermission()
        {
            RoleId = adminRole.Id,
            PermissionId = adminPermission.Id
        };
        db.RolePermissions.Add(adminRolePermission);
        db.SaveChanges();
    }
    private static Role CreateAdminRole(DsKitsmContext db)
    {
        var adminRole = new Role()
        {
            RoleName = "Admin",
            RoleDescription = "Admin Role"
        };
        db.Roles.Add(adminRole);
        db.SaveChanges();
        return adminRole;
    }
    private static Role CreateUserRole(DsKitsmContext db)
    {
        var role = new Role()
        {
            RoleName = "User",
            RoleDescription = "User Role"
        };
        db.Roles.Add(role);
        db.SaveChanges();
        return role;
    }
    private static Permission CreateAdminPermission(DsKitsmContext db)
    {
        var adminPermission = new Permission()
        {
            PermissionName = "Admin",
            PermissionDescription = "Admin Permission"
        };
        db.Permissions.Add(adminPermission);
        db.SaveChanges();
        return adminPermission;
    }
}