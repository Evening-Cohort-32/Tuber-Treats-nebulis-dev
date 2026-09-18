using TuberTreats.Models;

List<TuberDriver> tuberDrivers = new List<TuberDriver>
{
    new TuberDriver
    {
        Id = 1,
        Name = "Davey",
    },
    new TuberDriver
    {
        Id = 2,
        Name = "Jeraldi"
    },
    new TuberDriver
    {
        Id = 3,
        Name = "Constance"
    }
};

List<Customer> customers = new List<Customer>
{
    new Customer
    {
        Id = 1,
        Name = "Esposito",
        Address = "250 Maletre Kd."
    },
    new Customer
    {
        Id = 2,
        Name = "Providence",
        Address = "5 Petrichor Pl."
    },
    new Customer
    {
        Id = 3,
        Name = "Karl",
        Address = "420 DeepRock Rd."
    },
    new Customer
    {
        Id = 4,
        Name = "Ranni",
        Address = "108 Lieurnia Lk."
    },
    new Customer
    {
        Id = 5,
        Name = "Baek",
        Address = "297 RoyalCourt Ct. Apt. 11"
    }
};

List<Topping> toppings = new List<Topping>
{
    new Topping
    {
        Id = 1,
        Name = "Pertangle"
    },
    new Topping
    {
        Id = 2,
        Name = "Movember"
    },
    new Topping
    {
        Id = 3,
        Name = "Bloop"
    },
    new Topping
    {
        Id = 4,
        Name = "Racist Green"
    },
    new Topping
    {
        Id = 5,
        Name = "Mystery Topping"
    }
};

List<TuberOrder> tuberOrders = new List<TuberOrder>
{
    new TuberOrder
    {
        Id = 1,
        OrderPlacedOnDate = new DateTime(2018, 08, 07),
        CustomerId = 1,
        TuberDriverId = 3,
        DeliveredOnDate = new DateTime(2019, 08, 28),
    },
    new TuberOrder
    {
        Id = 2,
        OrderPlacedOnDate = new DateTime(2024, 11, 20),
        CustomerId = 4,
        TuberDriverId = 3,
        DeliveredOnDate = new DateTime(2025, 12, 05)
    },
    new TuberOrder
    {
        Id = 3,
        OrderPlacedOnDate = new DateTime(2001, 09, 11),
        CustomerId = 5,
        TuberDriverId = null,
        DeliveredOnDate = null
    },
    new TuberOrder
    {
        Id = 4,
        OrderPlacedOnDate = new DateTime(2015, 02, 14),
        CustomerId = 2,
        TuberDriverId = 1,
        DeliveredOnDate = null
    },
    new TuberOrder
    {
        Id = 5,
        OrderPlacedOnDate = new DateTime(1990, 05, 09),
        CustomerId = 3,
        TuberDriverId = 2,
        DeliveredOnDate = new DateTime(1995, 07, 19)
    }
};

List<TuberTopping> tuberToppings = new List<TuberTopping>
{
    new TuberTopping
    {
        Id = 1,
        TuberOrderId = 1,
        ToppingId = 2
    },
    new TuberTopping
    {
        Id = 2,
        TuberOrderId = 1,
        ToppingId = 5
    },
    new TuberTopping
    {
        Id = 3,
        TuberOrderId = 2,
        ToppingId = 3
    },
    new TuberTopping
    {
        Id = 4,
        TuberOrderId = 3,
        ToppingId = 1
    },
    new TuberTopping
    {
        Id = 5,
        TuberOrderId = 5,
        ToppingId = 4
    }
};

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

//add endpoints here

app.Run();
//don't touch or move this!
public partial class Program { }