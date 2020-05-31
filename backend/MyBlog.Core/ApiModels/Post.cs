namespace MyBlog.Core.ApiModels.Post
{
    public class ListItemPostModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
    
    public class ShowPostModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
    
    public class CreatePostModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
    
    public class UpdatePostModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
