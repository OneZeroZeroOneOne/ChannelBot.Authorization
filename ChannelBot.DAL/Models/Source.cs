using System;
using System.Collections.Generic;

namespace ChannelBot.DAL.Models
{
    public partial class Source
    {
        public Source()
        {
            Content = new HashSet<Content>();
            GroupSource = new HashSet<GroupSource>();
        }

        public string SourceUrl { get; set; }
        public int Id { get; set; }
        public int? PlatformId { get; set; }

        public virtual ICollection<Content> Content { get; set; }
        public virtual ICollection<GroupSource> GroupSource { get; set; }
    }
}
