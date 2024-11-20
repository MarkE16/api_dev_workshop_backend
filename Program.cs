using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add CORS - Cross-Origin Resource Sharing
// CORS is a security feature that restricts what resources a web page can request from another domain.
// CORS prevents cross-origin attacks by restricting the resources that a web page can request from another domain.
// This ensures that a web page can only request resources from the same domain,
// making it difficult for an attacker to access sensitive information from another domain.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        // In a production application, you should only allow specific origins.
        // However, for the sake of this workshop, we will allow any origin.
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

// Add Controllers (API Endpoints)
builder.Services.AddControllers();

// Add the DB context.
// This line of code allows the application to use the TodoContext to interact with the database.
builder.Services.AddDbContext<TodoContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});


// Add the Swagger services.
// This code adds the Swagger services to the application. Swagger is a tool that helps you document your API.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Build the application.
var app = builder.Build();

// If the application is running in a development environment, enable the Swagger middleware.
// This middleware generates the Swagger documentation for the API.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS
app.UseCors();

app.UseHttpsRedirection();
app.UseAuthorization();

// This line of code tells the application to use the controllers that were added to the application.
// This allows the application to respond to HTTP requests.
app.MapControllers();

app.Run();
