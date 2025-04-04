using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entitities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32.SafeHandles;

namespace API.Controllers
{
    public class BuggyController(DataContext context) : BaseApiController
    {
        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetAuth()
        {
            return "Secret text";
        }
        
                
        [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound()
        {
            var thing=context.Users.Find(-1);
            if (thing==null) return NotFound();
            return thing;
        }
        
        [HttpGet("server-error")]
        public ActionResult<AppUser> GetServerError()
        {
            
                var thing=context.Users.Find(-1) ?? throw new Exception ("A bad thing has returned");
              return thing;
             
            
        }
         [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("This was not a good request");
        }
        

    }
}