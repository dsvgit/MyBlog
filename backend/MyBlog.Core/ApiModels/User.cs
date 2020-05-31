using System.Collections.Generic;

namespace MyBlog.Core.ApiModels.User
{
    public class ListItemUserModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        // public IEnumerable<string> Roles { get; set; }
    }
    
    public class ShowUserModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> RoleIds { get; set; }
    }
    
    public class CreateUserModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public IEnumerable<string> RoleIds { get; set; }
    }
    
    public class UpdateUserModel
    {
        public IEnumerable<string> RoleIds { get; set; }
    }
}