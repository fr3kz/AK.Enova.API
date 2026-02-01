using AK.Enova.API;
using Microsoft.AspNetCore.Builder;

EnovaBootLoader.Init();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEnovaApi(builder.Configuration);

var app = builder.Build();

app.UseEnovaApi();

app.Run("http://localhost:5000");
