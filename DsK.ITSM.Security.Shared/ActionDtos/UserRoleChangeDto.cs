namespace DsK.ITSM.Security.Shared
{
    public class UserRoleChangeDto
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public bool RoleEnabled { get; set; }
    }
}
