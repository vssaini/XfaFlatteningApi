using XfaFlatteningApi.Contracts;
using XfaFlatteningApi.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IFlattenerUtil, FlattenerUtil>();
builder.Services.AddSingleton<IFoxitUtil, FoxitUtil>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

var foxitUtil = app.Services.GetRequiredService<IFoxitUtil>();
foxitUtil.InitLibrary();

app.Run();