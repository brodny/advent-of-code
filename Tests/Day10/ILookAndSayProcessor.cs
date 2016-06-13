namespace Tests.Day10
{
    public interface ILookAndSayProcessor
    {
        string Process(string input);
        string ProcessIteratively(string input, int numberOfIterations);
    }
}