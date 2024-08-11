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
            Game.Scene = Scene;
            _formSize = Size;
        }

        public static Scene Scene { get; internal set; }

        public static float DeltaTime { get; private set; }

        public void Run()
        {
            _formManager = new FormManager(_name, _formSize);

            _gameThread = new Thread(Handler);
            _gameThread.Start();

            _formManager.Run();
        }

        public void Stop()
        {
            _gameThread.Abort();
        }

        private void Handler()
        {
            while (!_formManager.IsShown) { }

            FPS fps = new FPS();

            while (true)
            {
                var stopwatch = Stopwatch.StartNew();

                BodyHandler();

                stopwatch.Stop();

                fps.AddTik(Convert.ToInt32(stopwatch.ElapsedMilliseconds));
                DeltaTime = 60 / Convert.ToSingle(fps.GetFPS());
                Console.WriteLine("FPS : {0}", fps.GetFPS());
            }
        }

        private void BodyHandler()
        {
            GraphicsRender.BeginDraw();
            GraphicsRender.Clear();

            Scene.Update();

            GraphicsRender.EndDraw();
        }
    }
}
