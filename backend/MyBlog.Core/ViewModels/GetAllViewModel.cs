namespace MyBlog.Core.ViewModels
{
    public class GetAllViewModel
    {
        public int Page { get; set; }
        public int Size { get; set; }

        public int Skip => Page * Size;
    }
}
