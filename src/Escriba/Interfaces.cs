using Escriba;

namespace Escriba
{
    public interface ICustomLogger
    {
        void send(string prefix, object content);
    }
}