namespace MyBlog.Core.ApiModels.Role
{
    public class ListItemRoleModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
    
    public class ShowRoleModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
    
    public class CreateRoleModel
    {
        public string Name { get; set; }
    }
    
    public class UpdateRoleModel
    {
        public string Name { get; set; }
    }
}
