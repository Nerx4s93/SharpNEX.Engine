using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;

using SharpNEX.Engine.Components;

namespace SharpNEX.Engine
{
    public class Game
    {
        private readonly string _name;

        private FormManager _formManager;

        private Thread _gameThread;

        public Game(string Name, Scene Scene, Size Size)
        {
            _name = Name;
            this.Scene = Scene;

            _formManager = new FormManager(this, Name, Size);
            _formManager.Run();
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
