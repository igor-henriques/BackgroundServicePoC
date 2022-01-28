namespace BackgroundServicePoC.Utils;

public class Randomizer
{
    public static int GetRandomUInt(int initialIntervalInMilisseconds = 500, int finalIntervalInMillseconds = 5000)
        => new Random().Next(initialIntervalInMilisseconds, finalIntervalInMillseconds);
}