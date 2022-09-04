using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatToWatch.Entities.Dtos.Movie;

namespace WhatToWatch.Business.Abstract
{
    public interface IMailService
    {
        void SendMail(string mail,string userName,MovieDto movieDto);

    }
}
