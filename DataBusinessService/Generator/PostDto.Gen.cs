using System.ComponentModel;
using System.Collections.Generic;
namespace DataBusinessService.Model
{
	public partial class PostDto : INotifyPropertyChanged  
	{
		#region private
			private int _postId;
		private string _title;
		private string _content;
		private int _blogId;
		private DataBusinessService.Model.BlogDto _blog;
		#endregion

		#region public
		public int PostId
		{
			get { return _postId; }
			set
			{
				if (Equals(_postId, value)) return;
				_postId = value;
				OnPropertyChanged("PostId");
			}
		}
		public string Title
		{
			get { return _title; }
			set
			{
				if (Equals(_title, value)) return;
				_title = value;
				OnPropertyChanged("Title");
			}
		}
		public string Content
		{
			get { return _content; }
			set
			{
				if (Equals(_content, value)) return;
				_content = value;
				OnPropertyChanged("Content");
			}
		}
		public int BlogId
		{
			get { return _blogId; }
			set
			{
				if (Equals(_blogId, value)) return;
				_blogId = value;
				OnPropertyChanged("BlogId");
			}
		}
		public DataBusinessService.Model.BlogDto Blog
		{
			get { return _blog; }
			set
			{
				if (Equals(_blog, value)) return;
				_blog = value;
				OnPropertyChanged("Blog");
			}
		}
		#endregion

	    public static void InitializeMapper()
	    {
            AutoMapper.Mapper.CreateMap<Post, PostDto>();
            AutoMapper.Mapper.CreateMap<PostDto, Post>();
        }

		#region INotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName)
		{
			var handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion		
	}

    public static class PostExtension
    {
        public static IEnumerable<PostDto> ToDtos(this IEnumerable<Post> objPosts)
        {
            return objPosts.MapToList<PostDto>();
        }
        public static PostDto ToDto(this Post objPost)
        {
            return objPost.MapTo<PostDto>();
        }

        public static IEnumerable<Post> ToDbs(this IEnumerable<PostDto> dtoPosts)
        {
            return dtoPosts.MapToList<Post>();
        }
        public static Post ToDb(this PostDto dtoPost)
        {
            return dtoPost.MapTo<Post>();
        }
    }
    public interface IPostService
    {
    }
}
