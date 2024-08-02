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
        private Size _formSize;

        private Thread _gameThread;

        public Game(string Name, Scene Scene, Size Size)
        {
            _name = Name;
            this.Scene = Scene;
            _formSize = Size;
        }

        public Scene Scene;

        public void Run()
        {
            _formManager = new FormManager(this, _name, _formSize);

            _gameThread = new Thread(Handler);
            _gameThread.Start();

            _formManager.Run();
        }

        public void Stop()
        {
            _gameThread.Abort();
        }

        public void Handler()
        {
            while (!_formManager.IsShown) { }

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
