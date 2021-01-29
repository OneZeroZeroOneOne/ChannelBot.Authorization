using System;
using System.Collections.Generic;

namespace ChannelBot.DAL.Models
{
    public partial class Content
    {
        public long Id { get; set; }
        public string MediaUrl { get; set; }
        public string Description { get; set; }
        public int SourceId { get; set; }

        public virtual Source Source { get; set; }
    }
}
