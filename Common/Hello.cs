using System;

namespace Common
{
    public interface IHello
    {
        void Say();
    }

    public class HelloBase : IHello
    {
        public virtual void Say() { Console.WriteLine("hola hoooo"); }
    }
}
