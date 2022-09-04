using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatToWatch.Core.Exceptions
{
    public class UnAuthorizeException : Exception
    {
        public UnAuthorizeException()
        {

        }
        public UnAuthorizeException(string name) : base(name)
        {

        }
    }
}
