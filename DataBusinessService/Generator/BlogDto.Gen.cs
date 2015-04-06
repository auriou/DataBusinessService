
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.Generic;
namespace DataBusinessService.Model
{
	public partial class BlogDto : INotifyPropertyChanged  
	{
		#region private
			private System.Collections.Generic.List<DataBusinessService.Model.PostDto> _posts;
		private int _blogId;
		private string _name;
		private string _url;
		#endregion

		#region public
		public System.Collections.Generic.List<DataBusinessService.Model.PostDto> Posts
		{
			get { return _posts; }
			set
			{
				if (Equals(_posts, value)) return;
				_posts = value;
				OnPropertyChanged("Posts");
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
		public string Name
		{
			get { return _name; }
			set
			{
				if (Equals(_name, value)) return;
				_name = value;
				OnPropertyChanged("Name");
			}
		}
		public string Url
		{
			get { return _url; }
			set
			{
				if (Equals(_url, value)) return;
				_url = value;
				OnPropertyChanged("Url");
			}
		}
		#endregion

	    public static void InitializeMapper()
	    {
            AutoMapper.Mapper.CreateMap<Blog, BlogDto>();
            AutoMapper.Mapper.CreateMap<BlogDto, Blog>();
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

    public static class BlogExtension
    {
        public static IEnumerable<BlogDto> ToDtos(this IEnumerable<Blog> objBlogs)
        {
            return objBlogs.MapToList<BlogDto>();
        }
        public static BlogDto ToDto(this Blog objBlog)
        {
            return objBlog.MapTo<BlogDto>();
        }

        public static IEnumerable<Blog> ToDbs(this IEnumerable<BlogDto> dtoBlogs)
        {
            return dtoBlogs.MapToList<Blog>();
        }
        public static Blog ToDb(this BlogDto dtoBlog)
        {
            return dtoBlog.MapTo<Blog>();
        }
    }
    public interface IBlogService
    {
    }
}
