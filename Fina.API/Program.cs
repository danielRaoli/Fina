using Fina.API;
using Fina.API.Common;
using Fina.API.Data;
using Fina.API.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.AddConfiguration();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();


var app = builder.Build();

app.ConfigureDevEnvironment();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors(ApiConfiguration.CorsPolicyName);

app.MapEndpoints();


app.Run();

