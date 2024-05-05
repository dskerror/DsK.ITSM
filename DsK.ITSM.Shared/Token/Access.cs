using System.ComponentModel;
using System.Reflection;

namespace DsK.ITSM.Shared.Token;

public static class Access
{
    public const string Admin = "Admin";

    [DisplayName("Category")]
    [Description("Category Permissions")]
    public static class Category
    {
        public const string View = "Category.View";
        public const string Create = "Category.Create";
        public const string Edit = "Category.Edit";
        public const string Delete = "Category.Delete";
    }


    [DisplayName("ITSystem")]
    [Description("ITSystem Permissions")]
    public static class ITSystem
    {
        public const string View = "ITSystem.View";
        public const string Create = "ITSystem.Create";
        public const string Edit = "ITSystem.Edit";
        public const string Delete = "ITSystem.Delete";
    }

    [DisplayName("Priority")]
    [Description("Priority Permissions")]
    public static class Priority
    {
        public const string View = "Priority.View";
        public const string Create = "Priority.Create";
        public const string Edit = "Priority.Edit";
        public const string Delete = "Priority.Delete";
    }

    [DisplayName("RequestAssignedHistory")]
    [Description("RequestAssignedHistory Permissions")]
    public static class RequestAssignedHistory
    {
        public const string View = "RequestAssignedHistory.View";
        public const string Create = "RequestAssignedHistory.Create";
        public const string Edit = "RequestAssignedHistory.Edit";
        public const string Delete = "RequestAssignedHistory.Delete";
    }

    [DisplayName("RequestMessageHistory")]
    [Description("RequestMessageHistory Permissions")]
    public static class RequestMessageHistory
    {
        public const string View = "RequestMessageHistory.View";
        public const string Create = "RequestMessageHistory.Create";
        public const string Edit = "RequestMessageHistory.Edit";
        public const string Delete = "RequestMessageHistory.Delete";
    }

    [DisplayName("Request")]
    [Description("Request Permissions")]
    public static class Request
    {
        public const string View = "Request.View";
        public const string Create = "Request.Create";
        public const string Edit = "Request.Edit";
        public const string Delete = "Request.Delete";
    }

    [DisplayName("RequestStatusHistory")]
    [Description("RequestStatusHistory Permissions")]
    public static class RequestStatusHistory
    {
        public const string View = "RequestStatusHistory.View";
        public const string Create = "RequestStatusHistory.Create";
        public const string Edit = "RequestStatusHistory.Edit";
        public const string Delete = "RequestStatusHistory.Delete";
    }

    [DisplayName("Status")]
    [Description("Status Permissions")]
    public static class Status
    {
        public const string View = "Status.View";
        public const string Create = "Status.Create";
        public const string Edit = "Status.Edit";
        public const string Delete = "Status.Delete";
    }


    [DisplayName("User")]
    [Description("User Permissions")]
    public static class User
    {
        public const string View = "User.View";
        public const string Create = "User.Create";
        public const string Edit = "User.Edit";
        public const string Delete = "User.Delete";
    }

    /// <summary>
    /// Returns a list of Permissions.
    /// </summary>
    /// <returns></returns>
    public static List<string> GetRegisteredPermissions()
    {
        var permissions = new List<string>();
        foreach (var prop in typeof(Access).GetNestedTypes().SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
        {
            var propertyValue = prop.GetValue(null);
            if (propertyValue is not null)
                permissions.Add(propertyValue.ToString());
        }
        return permissions;
    }
}