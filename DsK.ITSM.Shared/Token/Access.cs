﻿using System.ComponentModel;
using System.Reflection;

namespace DsK.ITSM.Shared.Token;

public static class Access
{
    public const string Admin = "Admin";

    [DisplayName("Request")]
    [Description("Request Permissions")]
    public static class RequestPage
    {
        public const string RequestFunction = "DsK.ITSM.Request";
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