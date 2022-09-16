using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    abstract class BaseComponent
    {
        public abstract void DoWork();
    }

    class WorkComponent : BaseComponent
    {
        public override void DoWork()
        {
            Console.WriteLine("Do work ...");
        }
    }

    abstract class WorkDecoratorComponent : BaseComponent
    {
        protected BaseComponent baseComponent;

        public void SetComponent(BaseComponent baseComponent)
        {
            this.baseComponent = baseComponent;
        }

        public override void DoWork()
        {
            if (baseComponent != null)
                baseComponent.DoWork();
        }

    }

    class FinalWorkComponent1 : WorkDecoratorComponent
    {
        public override void DoWork()
        {
            if (base.baseComponent != null)
                base.baseComponent.DoWork();
            else
                Console.WriteLine("Final DoWork (1) ...");
        }
    }

    class FinalWorkComponent2 : WorkDecoratorComponent
    {
        public override void DoWork()
        {
            Console.WriteLine("Final DoWork (2) ...");
        }
    }
}
