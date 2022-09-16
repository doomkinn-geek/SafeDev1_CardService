using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    public class SystemA
    {
        public void DoProcessA1()
        {

        }

        public void DoProcessA2()
        {

        }

        public void DoProcessA3()
        {

        }
    }

    public class SystemB
    {
        public void DoProcessB1()
        {

        }

        public void DoProcessB2()
        {

        }
    }

    public class SystemC
    {
        public void DoProcessC()
        {

        }
    }

    public class Facade
    {
        private SystemA _systemA;
        private SystemB _systemB;
        private SystemC _systemC;

        public Facade(SystemA systemA, SystemB systemB, SystemC systemC)
        {
            _systemA = systemA;
            _systemB = systemB;
            _systemC = systemC;
        }

        public void DoWork1()
        {
            _systemA.DoProcessA1();
            _systemB.DoProcessB2();
        }

        public void DoWork2()
        {
            _systemA.DoProcessA3();
            _systemA.DoProcessA1();
            _systemC.DoProcessC();
        }

        public void DoWork3()
        {
            _systemB.DoProcessB2();
            _systemC.DoProcessC();
        }

    }
}
