using Microsoft.EntityFrameworkCore;
using ShoeService_Api.GraphQL;
using ShoeService_Data;
using ShoeService_Data.Infrastructure;
using ShoeService_Data.Mapping;
using ShoeService_Data.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("SqlConnection");
builder.Services.AddDbContext<ShoeServiceDbContext>(options => options.UseSqlServer(connectionString));

//CORS 
var AllowAll = "_AllowAll";

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: AllowAll,
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

//GraphQL
builder.Services
        .AddGraphQLServer()
        .RegisterDbContext<ShoeServiceDbContext>()
        .AddQueryType(x => x.Name("Query"))
        .AddTypeExtension<QueryShoes>()
        .AddProjections()
        .AddFiltering()
        .AddSorting();

//DI
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IShoeRepository, ShoeRepository>();

//Mapper
//Auto Mapper
builder.Services.AddAutoMapper(typeof(MapProfile));


builder.Services.AddControllers();
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

app.UseAuthorization();

app.UseRouting();
app.UseCors(AllowAll);


app.UseEndpoints(endpoints =>
{
    app.MapControllers();
    app.MapGraphQL();
    //app.MapHub<ChatHub>("/chat");
});


app.Run();
