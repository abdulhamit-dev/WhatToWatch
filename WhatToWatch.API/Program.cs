using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using WhatToWatch.Business.Abstract;
using WhatToWatch.Business.Concrete;
using WhatToWatch.Business.ValidationRules;
using WhatToWatch.Core.Extensions;
using WhatToWatch.Core.Utilities.Security.Encryption;
using WhatToWatch.Core.Utilities.Security.JWT;
using WhatToWatch.DataAccess.Abstract;
using WhatToWatch.DataAccess.Concrete.Repositories;
using WhatToWatch.Entities.Dtos.MovieNoteAndVote;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get< TokenOptions>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),
            ClockSkew = TimeSpan.Zero
        };

    });

builder.Services.AddSingleton<IUserService, UserManager>();
builder.Services.AddSingleton<IAuthService, AuthManager>();
builder.Services.AddSingleton<IMovieNoteAndVoteService, MovieNoteAndVoteManager>();
builder.Services.AddSingleton<IMovieService, MovieManager>();

builder.Services.AddSingleton<IUserDal, EfUserDal>();
builder.Services.AddSingleton<IMovieNoteAndVoteDal, EfMovieNoteAndVoteDal>();
builder.Services.AddSingleton<IMovieDal, EfMovieDal>();

builder.Services.AddSingleton<ITokenHelper, JwtTokenHelper>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IValidator<MovieNoteAndVoteAddDto>, MovieNoteAndVoteValidator>();

builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.CustomExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
