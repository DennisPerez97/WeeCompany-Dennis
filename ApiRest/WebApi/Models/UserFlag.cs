using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class UserFlag
    {
        public User User { get; set; }
        public String Message { get; set; }
        public bool Error { get; set; }
    }
}