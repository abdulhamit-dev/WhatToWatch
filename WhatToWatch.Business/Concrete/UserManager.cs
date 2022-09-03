using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatToWatch.Business.Abstract;
using WhatToWatch.Core.Utilities.Result;
using WhatToWatch.DataAccess.Abstract;
using WhatToWatch.Entities.Conrete;

namespace WhatToWatch.Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public IDataResult<User> GetByName(string userName)
        {
            return new SuccessDataResult<User>(_userDal.Get(x => x.UserName == userName));
        }

    }
}
