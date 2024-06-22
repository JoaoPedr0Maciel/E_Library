using E_Library.Application.Services;
using E_Library.Application.Services.Authentication;
using E_Library.Application.Services.Books;
using E_Library.Application.Services.Users;
using E_Library.Domain.Repositories;
using E_Library.Infrastructure;
using E_Library.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<BookServices>();
builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<PasswordServices>();
builder.Services.AddTransient<JwtServices>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy( corsPolicyBuilder =>
    {
         corsPolicyBuilder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.MapControllers();
app.UseHttpsRedirection();
app.Run();

