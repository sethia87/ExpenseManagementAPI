using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adds SQL Server Database Connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registers Services
builder.Services.AddScoped<IExpenseService, ExpenseService>();

// Enable Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configures CORS (For Frontend Access)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// Enables Swagger UI in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enables Middleware
app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");  
app.UseAuthorization();
app.MapControllers();

// Runs the API
app.Run();