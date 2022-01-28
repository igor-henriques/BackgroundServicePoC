namespace BackgroundServicePoC.Models;

public partial record Bolsa
{
    public Bolsa(ulong Id, string Customer)
    {
        this.Id = Id;
        this.Customer = Customer;
        this.EstimatedTaskDuration = Randomizer.GetRandomUInt();
    }

    public static List<Bolsa> GenerateList()
    {
        return Enumerable
            .Range(0, 100)
            .Select(i => new Bolsa((ulong)i, $"Customer Nº {i + 1}"))
            .ToList();
    }
}