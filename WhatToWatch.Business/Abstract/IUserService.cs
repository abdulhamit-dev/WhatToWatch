using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatToWatch.Core.Utilities.Result;
using WhatToWatch.Entities.Conrete;

namespace WhatToWatch.Business.Abstract
{
    public interface IUserService
    {
        IDataResult<User> GetByName(string userName);
    }
}
