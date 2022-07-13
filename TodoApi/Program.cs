using Microsoft.EntityFrameworkCore;
using UsersApi.UserContext;
using UsersApi.Controllers;

var MyCorsOrigins = "_myCorsOrigins";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: MyCorsOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:5500", "http://localhost:8080");
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UserDb>(opt => opt.UseInMemoryDatabase("UserDB"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();
app.UseCors(MyCorsOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/users", UserController.GetAll);
app.MapGet("/users/{id}", UserController.GetDetail);
app.MapGet("/users/isAdmin", UserController.GetAdmins);
app.MapPost("/users", UserController.Create);
app.MapPut("/users/{id}", UserController.ModifyWhole);
app.MapDelete("/users/{id}", UserController.Remove);

app.Run();