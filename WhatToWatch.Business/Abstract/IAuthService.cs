using WhatToWatch.Core.Entities;
using WhatToWatch.Core.Utilities.Result;
using WhatToWatch.Core.Utilities.Security.JWT;
using WhatToWatch.Entities.Dtos.User;

namespace WhatToWatch.Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Login(UserLoginDto userLoginDto);
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
