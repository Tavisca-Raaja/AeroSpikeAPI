using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AerospikeAPI.Models
{
    public class UpdateContent
    {
        public string tweetId { get; set; }
        public string binName { get; set; }
        public string binContent { get; set; }
    }
}