using System;
using System.Collections.Generic;

namespace ChannelBot.DAL.Models
{
    public partial class GroupSource
    {
        public int SourceId { get; set; }
        public int GroupId { get; set; }

        public virtual Group Group { get; set; }
        public virtual Source Source { get; set; }
    }
}
