using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelBot.DAL.Models
{
    public class JwtOption
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public int Id { get; set; }
    }
}
