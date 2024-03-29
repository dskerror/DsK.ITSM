﻿namespace DsK.ITSM.Client.Services.Routes
{
    public static class RoleEndpoints
    {
        public static string Get(int id, int pageNumber, int pageSize, string searchString, string[] orderBy)
        {
            var url = $"api/security/roles?Id={id}&pageNumber={pageNumber}&pageSize={pageSize}&searchString={searchString}&orderBy=";
            if (orderBy?.Any() == true)
            {
                foreach (var orderByPart in orderBy)
                {
                    url += $"{orderByPart},";
                }
                url = url[..^1]; // loose training ,
            }
            return url;
        }
                
        public static string Post = "api/security/roles";
        public static string Put = "api/security/roles";
        public static string Delete = "api/security/roles";
    }
}
