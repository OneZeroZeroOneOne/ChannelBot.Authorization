using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelBot.DAL.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public int Role { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
