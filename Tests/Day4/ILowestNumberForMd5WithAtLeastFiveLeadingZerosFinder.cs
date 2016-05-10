namespace Tests.Day4
{
    public interface ILowestNumberForMd5WithAtLeastFiveLeadingZerosFinder
    {
        int FindLowestNumber(string secretKey, byte leadingZeros);
    }
}