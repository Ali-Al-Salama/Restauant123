using Microsoft.EntityFrameworkCore;
using Restaurant.Middleware;
using Restaurant.Services.Implementation;
using Restaurant.Services;
using Restaurant.utils;
using Restaurant.Persistence;
using Restaurant;
using restaurant.utils;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<auth>();
builder.Services.AddTransient<RestaurantException>();
builder.Services.AddTransient<SendConfirmCode>();
builder.Services.AddScoped<RestaurantService>();
builder.Services.AddScoped<WeeklyMenuService>();
builder.Services.AddScoped<MealService>();
builder.Services.AddScoped<OrderServices>();
builder.Services.AddScoped<ManagerRestaurantServices>();
builder.Services.AddScoped<ManagerWeeklyMenuServices>();
builder.Services.AddScoped<ManagerOrderServices>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<ResetPasswordService>();
builder.Services.AddScoped<AccessTokenService>();
builder.Services.AddScoped<ConvertString2Long>();
builder.Services.AddScoped<Encrypt>();
builder.Services.AddScoped<Decrypt>();
builder.Services.AddScoped<Token>();
builder.Services.AddScoped<Role>();
builder.Services.AddScoped<RandomString>();
builder.Services.AddScoped<LoginMessageResponse>();
builder.Services.AddScoped<TokenMessageResponse>();
builder.Services.AddScoped<InternalError>();
builder.Services.AddScoped<Category>();
builder.Services.AddScoped<RegisterErrorResponse>();
builder.Services.AddScoped<UploadPicture>();
builder.Services.AddScoped<WrongPassword>();
builder.Services.AddScoped<user_Service>();
builder.Services.AddScoped<user_Payment>();
builder.Services.AddScoped<manager_Payment>();
builder.Services.AddScoped<user_Pending>();
builder.Services.AddScoped<user_Register>();
builder.Services.AddScoped<order_Status>();
builder.Services.AddLogging();
var connectionString = builder.Configuration.GetConnectionString("AppDBConnectionString");

builder.Services.AddDbContext<AppDBContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var app = builder.Build();



// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

app.UseExceptionHandler("/error");
app.UseAuthorization();
app.UseMiddleware<RestaurantException>();
app.UseMiddleware<auth>();
app.MapControllers();
app.Run();