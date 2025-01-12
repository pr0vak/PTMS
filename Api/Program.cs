using Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFluentValidation();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGenCustomConfig();
builder.Services.AddPostgreSqlServiceConnection(builder.Configuration);
builder.Services.AddPostgreSqlIdentityContext();
builder.Services.AddConfigureIdentityOptions();
builder.Services.AddJwtTokenGenerator();
builder.Services.AddAuthenticationConfig(builder.Configuration);
builder.Services.AddCors();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();

app.UseCors(c =>
{
    c.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin()
        .WithExposedHeaders("*");
});

app.UseAuthentication();
app.UseAuthorization();

await app.Services.InitializeRoleAsync();

app.Run();
