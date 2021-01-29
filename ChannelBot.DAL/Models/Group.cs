using System;
using System.Collections.Generic;

namespace ChannelBot.DAL.Models
{
    public partial class Group
    {
        public Group()
        {
            GroupSource = new HashSet<GroupSource>();
        }

        public int CategoryId { get; set; }
        public int Id { get; set; }
        public int? SerialNumber { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<GroupSource> GroupSource { get; set; }
    }
}
