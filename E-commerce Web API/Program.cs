using E_commerce_Web_API.Controllers;
using E_commerce_Web_API.Data;
using E_commerce_Web_API.Repository.IRepository;
using E_commerce_Web_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure DbContext with connection string
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
//{
//    // Disable automatic model validation response
//    options.SuppressModelStateInvalidFilter = true;
//});

// Add service to the controller
builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        //var errors = context.ModelState.Where(error => error.Value != null && error.Value.Errors.Count > 0).Select(error => new
        //{
        //    Field = error.Key,
        //    Errors = error.Value != null ? error.Value.Errors.Select(x => x.ErrorMessage).ToArray() : new string[0]
        //}).ToList();

        // var errorString = string.Join("; ", errors.Select(error => $"{error.Field} : {string.Join(", ", error.Errors)}"));

        var errors = context.ModelState.Where(error => error.Value != null && error.Value.Errors.Count > 0).SelectMany(error => error.Value?.Errors != null ? error.Value.Errors.Select(e => e.ErrorMessage) : new List<string>()).ToList();

        return new BadRequestObjectResult(ApiResponse<object>.ErrorResponse(errors, 400, "Validation failed."));
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "API is working fine.");

app.MapControllers();
app.Run();

