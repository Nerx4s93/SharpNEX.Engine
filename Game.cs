using System;
using System.Diagnostics;
using System.Threading;

namespace SharpNEX.Engine
{
    public class Game
    {
        private readonly string _name;

        private Thread _gameThread;

        public Game(string Name, Scene Scene)
        {
            _name = Name;
            this.Scene = Scene;
        }

        public Scene Scene;

        public void Run()
        {
            _gameThread = new Thread(Handler);
            _gameThread.Start();
        }

        public void Stop()
        {
            _gameThread.Abort();
        }

        public void Handler()
        {
            FPS fps = new FPS();

            while (true)
            {
                var stopwatch = Stopwatch.StartNew();

                Scene.Update();

                stopwatch.Stop();

                fps.AddTik(Convert.ToInt32(stopwatch.ElapsedMilliseconds));
                Console.WriteLine(fps.GetFPS());
            }
        }
    }
}
