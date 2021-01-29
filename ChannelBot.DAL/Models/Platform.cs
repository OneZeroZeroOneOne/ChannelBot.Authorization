using System;
using System.Collections.Generic;

namespace ChannelBot.DAL.Models
{
    public partial class Platform
    {
        public Platform()
        {
            UserCredential = new HashSet<UserCredential>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }

        public virtual ICollection<UserCredential> UserCredential { get; set; }
    }
}
