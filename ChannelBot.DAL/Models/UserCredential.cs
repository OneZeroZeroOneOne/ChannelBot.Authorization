using System;
using System.Collections.Generic;

namespace ChannelBot.DAL.Models
{
    public partial class UserCredential
    {
        public int CategoryId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public int PlatformId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Platform Platform { get; set; }
    }
}
