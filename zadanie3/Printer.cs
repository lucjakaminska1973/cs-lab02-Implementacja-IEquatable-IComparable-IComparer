using System;
using System.Collections.Generic;
using System.Text;
using ver1;

namespace zadanie3
{
    public class Printer : IPrinter
    {
        protected IDevice.State state = IDevice.State.off;
        public int Counter { get; set; }
        public int PrintCounter { get; set; }


        public IDevice.State GetState()
        {
            return state;
        }

        public void PowerOff()
        {
            if (state == IDevice.State.on)
            {
                state = IDevice.State.off;
            }
        }

        public void PowerOn()
        {
            if (state == IDevice.State.off)
            {
                state = IDevice.State.on;
                Counter++;
            }
        }

        public void Print(in IDocument document)
        {
            if (state == IDevice.State.on)
            {
                PrintCounter++;
                Console.WriteLine("{0} Print: {1}",
                    DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss"),
                    document.GetFileName());
            }
        }
    }
}
