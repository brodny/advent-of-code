namespace Tests.Day5
{
    public enum StringType
    {
        Naughty,
        Nice,
    }

    public interface INaughtyOrNiceStringRecognizer
    {
        StringType RecognizeStringType(string input);
    }
}