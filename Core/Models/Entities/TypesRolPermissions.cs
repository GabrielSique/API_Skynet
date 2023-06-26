namespace Core.Models.Entities
{
    public class TypesRolPermissions
    {
        public int idRol { get; set; }
        public string? rolName { get; set; }
        public string createUser { get; set; }
        public string updateUser { get; set; }
        public string readUser { get; set; }
        public string createVisit { get; set; }
        public string updateVisit { get; set; }
        public string reportClient { get; set; }
        public string reportSuper { get; set; }
        public string sendEmail { get; set; }
    }
}
