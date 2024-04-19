using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GLTester
{
    internal class TimeCounter
    {
        private readonly IActualizable refToActualizeable;

        private readonly TimerCallback CallBack;

        private readonly Timer Counter;



        public void Operation(object obj) => refToActualizeable.Actualize();
        



        public TimeCounter(IActualizable refToActualizeable, int period)
        {
            CallBack = new TimerCallback(Operation);

            this.refToActualizeable = refToActualizeable;

            Counter = new Timer(CallBack, null, 0, period);
        }
        



    }
}
