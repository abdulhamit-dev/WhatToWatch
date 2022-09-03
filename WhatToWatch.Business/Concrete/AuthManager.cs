using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatToWatch.Business.Abstract;
using WhatToWatch.Core.Entities;
using WhatToWatch.Core.Utilities.Result;
using WhatToWatch.Core.Utilities.Security.JWT;
using WhatToWatch.Entities.Dtos.User;

namespace WhatToWatch.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;
        public AuthManager(ITokenHelper tokenHelper, IUserService userService)
        {
            _tokenHelper = tokenHelper;
            _userService = userService;
        }
        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var accessToken = _tokenHelper.CreateToken(user);
            return new SuccessDataResult<AccessToken>(accessToken, "Token oluşturuldu");
        }

        public IDataResult<User> Login(UserLoginDto userLoginDto)
        {
            var user = _userService.GetByName(userLoginDto.UserName);

            if (user.Data == null)
            {
                return new ErrorDataResult<User>("Kullanıcı Bulunamadı");
            }
            //to do mapper kullan
            User newUser = new User
            {
                Password = user.Data.Password,
                UserName = user.Data.UserName,
                Id = user.Data.Id,
                FirstName = user.Data.Name,
                LastName = user.Data.Surname
            };

            //User newUser = new User();


            return new SuccessDataResult<User>(newUser, "Başarılı giriş");
        }
    }
}
