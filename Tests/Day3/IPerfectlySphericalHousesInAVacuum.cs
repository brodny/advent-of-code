namespace Tests.Day3
{
    public interface IPerfectlySphericalHousesInAVacuum
    {
        void Process(string input);

        int HousesWithAtLeastOnePresent { get; }
        int NumberOfAgents { get; set; }
    }
}