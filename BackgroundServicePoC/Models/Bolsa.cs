namespace BackgroundServicePoC.Models;

public record Bolsa
{
    public ulong Id { get; private init; }
    public string Customer { get; private init; }
    public uint EstimatedTaskDuration { get; private init; }

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