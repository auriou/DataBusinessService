using System.Collections.Generic;

namespace DataBusinessService.Model
{
    //[AutoProperty("Test", typeof(string))]
    public class Blog : IDtoGen
    {
        public virtual List<Post> Posts { get; set; }
        public int BlogId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        
    }
}