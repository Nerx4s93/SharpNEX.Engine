using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;

using SharpNEX.Engine.Components;

namespace SharpNEX.Engine
{
    public static class Game
    {
        private static HandleManager _formManager;

        private static Thread _gameThread;

        public static bool IsGameRun { get; private set; }

        public static Scene Scene { get; internal set; }

        public static float DeltaTime { get; private set; }

        public static void Run(Scene Scene, IntPtr Handle, Size Size)
        {
            IsGameRun = true;

            Game.Scene = Scene;

            _formManager = new HandleManager(Handle, Size);
            _formManager.Run();

            _gameThread = new Thread(Handler);
            _gameThread.Start();
        }

        public static void Stop()
        {
            IsGameRun = false;
            _gameThread.Abort();
        }

        private static void Handler()
        {
            while (!_formManager.IsRuned) { }

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
