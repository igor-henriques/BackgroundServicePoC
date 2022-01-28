namespace BackgroundServicePoC.Models;

public partial record Bolsa
{
    public ulong Id { get; private set; }
    public string Customer { get; private set; }
    public uint EstimatedTaskDuration { get; private set; }    
}