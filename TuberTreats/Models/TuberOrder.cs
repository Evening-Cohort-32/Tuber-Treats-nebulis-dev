namespace TuberTreats.Models;

public class TuberOrder
{
    public int Id { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; }

    public int? TuberDriverId { get; set; }
    public TuberDriver TuberDriver { get; set; }

    public DateTime OrderPlacedOnDate { get; set; }
    public DateTime? DeliveredOnDate { get; set; }

    public List<Topping> Toppings { get; set; }
}