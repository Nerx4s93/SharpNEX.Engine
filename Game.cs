using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;

using SharpNEX.Engine.Components;

namespace SharpNEX.Engine
{
    public static class Game
    {
        private static string _name;

        private static FormManager _formManager;
        private static Size _formSize;

        private static Thread _gameThread;

        public static bool EditMode = false;

        public static Scene Scene { get; internal set; }

        public static float DeltaTime { get; private set; }

        public static void Run(string Name, Scene Scene, Size Size)
        {
            _name = Name;
            Game.Scene = Scene;
            _formSize = Size;

            _formManager = new FormManager(_name, _formSize);

            _gameThread = new Thread(Handler);
            _gameThread.Start();

            _formManager.Run();
        }

        public static void Stop()
        {
            _gameThread.Abort();
        }

        private static void Handler()
        {
            while (!_formManager.IsShown) { }

            var fps = new FPS();

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

        private static void BodyHandler()
        {
            GraphicsRender.BeginDraw();
            GraphicsRender.Clear();

            Scene.Update();

            GraphicsRender.EndDraw();
        }
    }
}
