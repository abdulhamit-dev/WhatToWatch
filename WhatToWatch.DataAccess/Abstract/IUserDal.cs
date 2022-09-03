using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatToWatch.Core.DataAccess.Abstract;
using WhatToWatch.Entities.Conrete;

namespace WhatToWatch.DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
 
    }
}
