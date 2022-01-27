namespace BackgroundServicePoC.Utils;

public class Randomizer
{
    public static uint GetRandomUInt(int initialIntervalInMilisseconds = 500, int finalIntervalInMillseconds = 5000)
        => (uint)new Random().Next(initialIntervalInMilisseconds, finalIntervalInMillseconds);
}