using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpNEX.Engine
{
    internal class FPS
    {
        public FPS()
        {
            for (int i = 0; i < 1000; i++)
            {
                tiks.Enqueue(10);
            }
        }

        private int sum = 1000;
        private Queue<int> tiks = new Queue<int>();

        public void AddTik(int tik)
        {
            tiks.Enqueue(tik);
            sum += tik;

            while (sum >= 1000)
            {
                sum -= tiks.Dequeue();
            }
        }

        public int GetFPS()
        {
            return tiks.Count;
        }
    }
}
