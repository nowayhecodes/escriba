namespace Escriba
{
    public interface ICustomLogger
    {
        void send(String prefix, object content);
    }

    public interface ILogType
    {
        string color(Color color);

        void prefix(Prefix prefix);
    }

    private enum Color
    {
        GREEN = "green",
        MAGENTA = "magenta",
        YELLOW = "yellow",
        RED = "red"
    }

    private enum Prefix
    {
        INFO,
        IMPORTANT,
        WARNING,
        ERROR,
    }
}