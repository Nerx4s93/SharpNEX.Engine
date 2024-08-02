using System.Collections.Generic;

namespace SharpNEX.Engine
{
    internal class FPS
    {
        private int sum = 0;
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
