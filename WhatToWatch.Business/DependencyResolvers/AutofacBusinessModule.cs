using Autofac;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatToWatch.Business.Abstract;
using WhatToWatch.Business.Concrete;
using WhatToWatch.Business.ValidationRules;
using WhatToWatch.Core.Caching;
using WhatToWatch.Core.Caching.Redis;
using WhatToWatch.Core.Utilities.Security.JWT;
using WhatToWatch.DataAccess.Abstract;
using WhatToWatch.DataAccess.Concrete.Repositories;
using WhatToWatch.Entities.Dtos.MovieNoteAndVote;

namespace WhatToWatch.Business.DependencyResolvers
{
    public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
            builder.RegisterType<AuthManager>().As<IAuthService>().SingleInstance();
            builder.RegisterType<MovieNoteAndVoteManager>().As<IMovieNoteAndVoteService>().SingleInstance();
            builder.RegisterType<MovieManager>().As<IMovieService>().SingleInstance();

            builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();
            builder.RegisterType<EfMovieNoteAndVoteDal>().As<IMovieNoteAndVoteDal>().SingleInstance();
            builder.RegisterType<EfMovieDal>().As<IMovieDal>().SingleInstance();

            builder.RegisterType<JwtTokenHelper>().As<ITokenHelper>().SingleInstance();
            builder.RegisterType<RabbitMqManager>().As<IRabbitMqService>().SingleInstance();
            builder.RegisterType<RedisCacheService>().As<ICacheService>().SingleInstance();
            builder.RegisterType<RedisServer>().SingleInstance();
            
            builder.RegisterType<MovieNoteAndVoteValidator>().As<IValidator<MovieNoteAndVoteAddDto>>().SingleInstance();

        }
    }
}
