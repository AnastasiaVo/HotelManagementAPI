using FavorParkHotelAPI;
using FavorParkHotelAPI.Application.RoomManagement;
using FavorParkHotelAPI.Data;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.ModelBindingMessageProvider.SetValueIsInvalidAccessor(x => "Invalid value for {0}");
    options.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(() => "Request is missing");

    options.Filters.Add(new AuthorizeFilter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureAuthorization();

builder.Services.ConfigureProblemDetails();

builder.Services.ConfigureSwagger();
builder.Services.ConfigureDatabase(builder.Configuration);

builder.Services.AddAutoMapper(typeof(RoomMapping));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseProblemDetails();

//app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.Run();
