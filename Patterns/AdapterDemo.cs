using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{   

    class MyClient
    {
        public void Request(Target target)
        {
            target.Request();
        }
    }

    class Adapter : Target
    {
        private SpecificTarget specificTarget = new SpecificTarget();

        public override void Request()
        {
            specificTarget.SpecificRequest();
        }
    }

    class Target
    {
        public virtual void Request()
        {
            Console.WriteLine("Do work (1) ....");
        }
    }

    class SpecificTarget
    {
        public void SpecificRequest()
        {
            Console.WriteLine("Do work (2) ....");
        }
    }
}
