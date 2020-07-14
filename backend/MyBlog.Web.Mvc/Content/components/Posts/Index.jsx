export default function PostsIndex({ initialData }) {
  return (
    <div>
      <ul class="list-unstyled">
        {initialData.map((post) => {
          return (
            <li class="media mb-4">
              <div class="media-body">
                <a class="h5 mt-0 mb-1 d-block" href={`/Posts/Show/${post.id}`}>
                  {post.title}
                </a>
                {post.text}
              </div>
            </li>
          );
        })}
      </ul>
    </div>
  );
}
