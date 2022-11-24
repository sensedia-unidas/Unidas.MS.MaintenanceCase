using Maintenance.Case.Application.ViewModels.MaintenanceCase;
using Unidas.MS.Maintenance.Case.Application.ViewModels.MaintenanceCase;

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





app.MapPost("/caseRequisitionSalesforce/v1/create", async (CaseSalesforce caseSalesforce) =>
{
    return Results.Ok(caseSalesforce);
});


app.MapPost("/caseRequisitionJira/v1/create", async (CaseJira caseJira) =>
{
    return Results.Ok(caseJira);
});



app.Run();

