using System.Collections.Generic;

namespace MyBlog.Core.DtoModels
{
    public class PagedResultDto<TEntity>
    {
        public int TotalCount { get; set; }
        public IEnumerable<TEntity> Items { get; set; }
    }
}