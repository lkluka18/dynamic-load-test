using System;
using Common;

namespace TestModule
{
    public class Hello : HelloBase
    {
        public override void Say()
        {
            Console.WriteLine("Hello from TestModule");
        }
    }
}
