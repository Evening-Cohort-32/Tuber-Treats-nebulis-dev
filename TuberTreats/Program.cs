using System.Formats.Tar;
using TuberTreats.Models;
using TuberTreats.Models.DTO;
// ^Calls on all files with a matching namespace

//Database Lists
List<TuberDriver> tuberDrivers = new List<TuberDriver>      // Initializes a new List of a certain class Model
{
    new TuberDriver         // Initializes a new class Object
    {
        Id = 1,             // Sets the Id of the new Object
        Name = "Davey",     // Sets the Name of the new Object
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
// Similar format is used for the following Lists below

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

// TuberOrder Endpoints

// Get All TuberOrders
app.MapGet("/api/tuberorders", () =>        // Sets GET API endpoint for communicating to the server and runs the following function with no request in the parameter
{
    return tuberOrders.Select(to => new TuberOrderDTO       // Goes through all of the items in the selected list and creates/returns a new DTO(Data Transferrable Object) for each object
    {
        Id = to.Id,                                         // Sets the Id of the DTO to the Id of the original Object
        CustomerId = to.CustomerId,                         // Sets the CustomerId of the DTO to the CustomerId of the original Object
        TuberDriverId = to.TuberDriverId,                   // Sets the TuberDriverId of the DTO to the TuberDriverId of the original Object
        OrderPlacedOnDate = to.OrderPlacedOnDate,           // Sets the Order Placement Date of the DTO to the date of the original Object 
        DeliveredOnDate = to.DeliveredOnDate                // Sets the Delivery Date of the DTO to the date of the original Object
    });
    // Similar format is used for other endpoints that get all objects in a selected List
});

// Get One TuberOrder by Id
app.MapGet("/api/tuberorders/{id}", (int id) =>     // Sets GET API endpoint like before, but with the request of id in the parameter
{
    TuberOrder tuberOrder = tuberOrders.FirstOrDefault(to => to.Id == id);      // Looks for the first Object in the selected list(tuberOrders) that matches the given parameter of id (requested in the endpoint string)
    if (tuberOrder == null)                                                     // If tuberOrder turns up as null, that means there is no TuberOrder object that matches id parameter
    {
        return Results.NotFound();                                              // Returns message to the client saying that the requested object is not found
    }

    Customer customer = customers.FirstOrDefault(c => c.Id == tuberOrder.CustomerId);                  // Looks for the first Object in the selected list(customers) that has the Id that matches the CustomerId of the previously selected TuberOrder
    TuberDriver tuberDriver = tuberDrivers.FirstOrDefault(td => td.Id == tuberOrder.TuberDriverId);    // Looks for the first Object in the selected list(tuberDrivers) that has the Id that matches the TuberDriverId of the previously selected TuberOrder

    List<Topping> toppingSelection = tuberToppings                      // Gathers a list of Topping objects based on the TuberToppings join table
        .Where(tt => tt.TuberOrderId == id)                             // Checks for which TuberTopping objects contain a TuberOrderId that matches the Id parameter
        .Select(tt => toppings.First(t => t.Id == tt.ToppingId))        // Checks for which Topping object contains an Id that matches the ToppingId parameter in the select TuberTopping join table 
        .ToList();                                                      // Pushes all selected Toppings to the list of toppingSelection

    return Results.Ok(new TuberOrderDTO                                 // Returns an OK results and executes creating a new TuberOrderDTO
    {
        Id = tuberOrder.Id,                                             // Sets the Id of the DTO
        CustomerId = tuberOrder.CustomerId,                             // Sets the CustomerId of the DTO
        Customer = new CustomerDTO                                      // Creates a new CustomerDTO based on the previously selected Customer Object
        {
            Id = customer.Id,                                           // Sets Customer DTO data
            Name = customer.Name,
            Address = customer.Address
        },
        TuberDriverId = tuberOrder.TuberDriverId,                       // Sets the TuberDriverId of the DTO
        TuberDriver = new TuberDriverDTO                                // Creates a new TuberDriverDTO based on the previously selected TuberDriver Object
        {
            Id = tuberDriver.Id,                                        // Sets TuberDriver DTO data
            Name = tuberDriver.Name
        },
        OrderPlacedOnDate = tuberOrder.OrderPlacedOnDate,               // Sets the Order Date Data of the DTO
        DeliveredOnDate = tuberOrder.DeliveredOnDate,                   // Sets the Delivery Date Data of the DTO
        Toppings = toppingSelection.Select(ts => new ToppingDTO         // Pushes DTOs of all of the previously selected toppings to the Toppings list in the Object
        {
            Id = ts.Id,
            Name = ts.Name
        }).ToList()
    });
});
// ^Similar format is used for other endpoints that gets one object by selected by id

// Post/Create TuberOrder
app.MapPost("/api/tuberorders", (TuberOrder tuberOrder) =>      // Sets a Post API endpoint with a parameter of a TuberOrder Object (Created in a Request Body)
{
    Customer customer = customers.FirstOrDefault(c => c.Id == tuberOrder.CustomerId);       // Gets the customer data to check if the CustomerId for the Order is valid, returns a BadRequest Result if CustomerId isn't valid
    if (customer == null)
    {
        return Results.BadRequest();
    }

    tuberOrder.Id = tuberOrders.Max(to => to.Id) + 1;       // Sets the Id of the Order
    tuberOrders.Add(tuberOrder);                            // Adds the Order to the tuberOrders list

    List<Topping> toppingSelection = tuberToppings          // Adds a List of toppings the same as before
        .Where(tt => tt.TuberOrderId == tuberOrder.Id)
        .Select(tt => toppings.First(t => t.Id == tt.ToppingId))
        .ToList();

    return Results.Created($"/api/tuberorders/{tuberOrder.Id}", new TuberOrderDTO       // Creates new TuberOrderDTO to be posted to the database
    {
        Id = tuberOrder.Id,                                                             // You know the drill at this point
        CustomerId = tuberOrder.CustomerId,
        Customer = new CustomerDTO
        {
            Id = customer.Id,
            Name = customer.Name,
            Address = customer.Address
        },
        OrderPlacedOnDate = DateTime.Now,                                               // Except for this, this is different, it sets the Order Date to be when the Post request is called
        Toppings = toppingSelection.Select(ts => new ToppingDTO
        {
            Id = ts.Id,
            Name = ts.Name
        }).ToList()
    });
});
// ^Similar format is used for other Post endpoints

// Edit/Assign TuberOrder
app.MapPut("/api/tuberorders/{id}", (int id, TuberOrder tuberOrder) =>
{
    TuberOrder orderToUpdate = tuberOrders.FirstOrDefault(to => to.Id == id);

    if (orderToUpdate == null)
    {
        return Results.NotFound();
    }
    if (id != tuberOrder.Id)
    {
        return Results.BadRequest();
    }

    orderToUpdate.CustomerId = tuberOrder.CustomerId;
    orderToUpdate.TuberDriverId = tuberOrder.TuberDriverId;
    orderToUpdate.DeliveredOnDate = tuberOrder.DeliveredOnDate;

    return Results.NoContent();
});

// Post Completed Order
app.MapPost("/api/tuberorders/{id}/complete", (int id) =>
{
    TuberOrder orderToComplete = tuberOrders.FirstOrDefault(to => to.Id == id);

    orderToComplete.DeliveredOnDate = DateTime.Now;
});

//Topping endpoints

//Get All Toppings
app.MapGet("/api/toppings", () =>
{
    return toppings.Select(t => new ToppingDTO
    {
       Id =  t.Id,
       Name = t.Name
    });
});

//Get One Topping
app.MapGet("/api/toppings/{id}", (int id) =>
{
    Topping topping = toppings.FirstOrDefault(t => t.Id == id);
    if (topping == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(new ToppingDTO
    {
        Id = topping.Id,
        Name = topping.Name
    });
});

//TuberToppings Endpoints

//Get All TuberToppings
app.MapGet("/api/tubertoppings", () =>
{
    return tuberToppings.Select(tt => new TuberToppingDTO
    {
        Id = tt.Id,
        TuberOrderId = tt.TuberOrderId,
        ToppingId = tt.ToppingId
    });
});

//Add TuberTopping to TuberOrder
app.MapPost("/api/tubertoppings", (TuberTopping tuberTopping) =>
{
    TuberOrder tuberOrder = tuberOrders.FirstOrDefault(to => to.Id == tuberTopping.TuberOrderId);
    if (tuberOrder == null)
    {
        return Results.BadRequest();
    }

    Topping topping = toppings.FirstOrDefault(t => t.Id == tuberTopping.ToppingId);
    if (topping == null)
    {
        return Results.BadRequest();
    }

    tuberTopping.Id = tuberToppings.Max(tt => tt.Id) + 1;
    tuberToppings.Add(tuberTopping);

    return Results.Ok(new TuberToppingDTO
    {
        Id = tuberTopping.Id,
        TuberOrderId = tuberTopping.TuberOrderId,
        ToppingId = tuberTopping.ToppingId
    });
});

//Remove TuberTopping from TuberOrder
app.MapDelete("/api/tubertoppings/{id}", (int id) =>
{
    TuberTopping toppingToDelete = tuberToppings.FirstOrDefault(tt => tt.Id == id);
    if (toppingToDelete == null)
    {
        return Results.NotFound();
    }
    else
    {
        return Results.Ok(tuberToppings.Remove(toppingToDelete));    
    }
});
app.Run();
//don't touch or move this!
public partial class Program { }