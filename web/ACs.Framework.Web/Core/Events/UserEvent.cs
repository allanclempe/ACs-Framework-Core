using System;
using System.Collections.Generic;
using System.Security.Claims;
using ACs.Framework.Web.Core.Infra;
using ACs.Security.Jwt;

namespace ACs.Framework.Web.Core.Events
{
    public interface IUserEvent : IEvent<UserActivated, User>, IEvent<UserInserted, User>, IEvent<UserAuthenticated, string>
    {
    }

    public class UserEvent : IUserEvent
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenProvider _jwtProvider;

        public UserEvent(IUserRepository userRepository, IJwtTokenProvider jwtProvider)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
        }

        public User DoIt(UserInserted model)
        {
            if (_userRepository.EmailExists(model.Email))
                throw new SystemLogicException(ExceptionMessage.UserEmailAlreadyExists, model.Email);

            var user = new User(model.FirstName, model.LastName, model.Email, model.Password);
            _userRepository.SaveAndFlush(user);
            return user;
        }

        public User DoIt(UserActivated model)
        {
            var user = _userRepository.GetById(model.Id);

            if (user == null)
                throw new SystemLogicException(ExceptionMessage.EntityNotFounded);

            Guid token;
            if (!Guid.TryParse(model.Token, out token))
                throw new SystemLogicException(ExceptionMessage.InvalidToken);

            user.Activate(token);

            _userRepository.Save(user);

            return user;
        }

        public string DoIt(UserAuthenticated model)
        {
            var user = _userRepository.GetByEmail(model.Email);

            if (user == null)
                throw new SystemLogicException(ExceptionMessage.EntityNotFounded);

            if (!user.IsValidPassword(model.Password))
                throw new SystemLogicException(ExceptionMessage.LoginFailure);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.EmailPrimary.Address),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
            };

            var now = DateTime.UtcNow;
            return  _jwtProvider.GetToken(now, now.AddMinutes(60), claims.ToArray());

        }
    }
}
