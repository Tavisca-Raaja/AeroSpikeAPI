using Aerospike.Client;
using AerospikeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AerospikeAPI.Controllers
{
    public class TweetsController : ApiController
    {
       
        [HttpGet]
        public void test()
        {

        }
        [HttpGet]
        [Route("get")]
        public IHttpActionResult getTweetsByIds([FromUri]string[] tweetId)
        {
            DataAccess extract = new DataAccess();
            List<Response> tweets=extract.getElementById(tweetId);
            return Ok(tweets);
        }

        [HttpPut]
        [Route("updateDetails")]
        public IHttpActionResult update([FromBody] UpdateContent data)
        {
            DataAccess extract = new DataAccess();
            if (extract.Update(data.tweetId, data.binName, data.binContent))
                return Ok("Data of bin " + data.binName + " has Been updated Successfully");

            else
                return BadRequest("Invalid tweetId:"+data.tweetId);
        }

        [HttpDelete]
        [Route("Remove")]
        public  IHttpActionResult DeleteTweet([FromUri]string tweetId)
        {
            DataAccess flush = new DataAccess();
            if (flush.clear(tweetId))
                return Ok("All data of tweetId "+tweetId+" has been removed successfully");
            return BadRequest("Invalid tweetId:" + tweetId);
        }
    }
}
