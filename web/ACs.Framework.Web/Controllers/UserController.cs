using System.Collections.Generic;
using ACs.Framework.Web.Core.Dtos;
using ACs.Framework.Web.Core.Events;
using ACs.Framework.Web.Core.Infra;
using ACs.Net.Mail;
using ACs.NHibernate.AspnetCore;
using Microsoft.AspNetCore.Mvc;

namespace ACs.Framework.Web.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageSender _messageSender;
        private readonly IUserEvent _userEvent;
        private readonly ISystemConfiguration _systemConfiguration;

        public UserController(IUserRepository userRepository, IMessageSender messageSender, IUserEvent userEvent, ISystemConfiguration systemConfiguration)
        {
            _messageSender = messageSender;
            _userEvent = userEvent;
            _systemConfiguration = systemConfiguration;
            _userRepository = userRepository;
        }

        // GET: api/values
        [HttpGet,SessionRequired]
        public IEnumerable<UserDto> Get()
        {
            return _userRepository.GetAll();
        }

        [HttpGet("notfound")]
        public IActionResult NotFound()
        {
            return base.NotFound();
        }

        // GET api/values/5
        [HttpGet("{id}"), SessionRequired]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost, SessionRequired]
        public  void Post([FromBody]UserInserted model)
        {
            var user = _userEvent.DoIt(model);

            //await _messageSender.SendEmailAsync(user.EmailPrimary.Address, _systemConfiguration.GetMailMessage(EmailTemplate.UserInserted)
            //    .SetParams(new { name = user.FirstName, url = Request.GetAbsoluteUri($"user/{user.Id}/{user.Token}") })
            //   );

        }

        [HttpPost("token_access"), SessionRequired]
        public IActionResult BearerToken([FromBody] UserAuthenticated model)
        {
            return Content(_userEvent.DoIt(model));
        }

        [HttpGet("{id}/activate/{token}"), SessionRequired]
        public IActionResult Activate(int id, string token)
        {
            var model = new UserActivated {Id = id, Token = token};

            _userEvent.DoIt(model);

            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
