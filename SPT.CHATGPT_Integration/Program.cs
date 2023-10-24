using SPT.CHATGPT_Integration.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/healthcheck", () => "Hello World! Your app is live.");

app.MapGet("/getchatgptresponse/{inputquery}", async (string inputquery) =>
{
    var response = await new ResponseGenerator().GenerateContent(inputquery);
    return Results.Ok(response);
});

app.Run();