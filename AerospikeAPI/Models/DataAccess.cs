using Aerospike.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AerospikeAPI.Models
{
    public class DataAccess
    {
        AerospikeClient client = null;
        string nameSpace =null;
        string setName = null;
        public DataAccess()
        {
            client = new AerospikeClient("18.235.70.103", 3000);
            nameSpace = "AirEngine";
            setName = "Raaja";
        }
        public List<Response> getElementById(string[] tweetIds)
        {
            List<Response> tweetsData = new List<Response>();
            Response data = null;
            foreach (var id in tweetIds)
            {
                data = new Response();
                var key = new Key(nameSpace, setName, id);
                Record result = client.Get(new WritePolicy(), key);
                if (result == null)
                    tweetsData.Add(data);
                else
                {
                    data.Tweet = result.GetValue("text").ToString();
                    data.TimeStamp = result.GetValue("created").ToString();
                    data.TweetId = result.GetValue("id").ToString();
                    tweetsData.Add(data);
                }
            }
            return tweetsData;
        }

        public bool Update(string id,string name,string content)
        {
            var key = new Key(nameSpace, setName, id);
            Record result = client.Get(new WritePolicy(), key);
            if (result == null)
                return false;
            client.Put(new WritePolicy(), key,new Bin[] { new Bin(name, content) });
            return true;
        }

        public bool clear(string id)
        {
            var key = new Key(nameSpace, setName, id);
            if (client.Delete(new WritePolicy(), key))
                return true;
            return false;
           
        }
    }
}