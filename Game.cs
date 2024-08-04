﻿using System;
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

        public static GraphicsRender GpaphicsRender;

        public Scene Scene { get; internal set; }

        public void Run()
        {
            GpaphicsRender = new GraphicsRender();
            _formManager = new FormManager(this, GpaphicsRender, _name, _formSize);

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
                Console.WriteLine("FPS : {0}", fps.GetFPS());
            }
        }

        private void BodyHandler()
        {
            GpaphicsRender.BeginDraw();
            GpaphicsRender.Clear();

            Scene.Update();

            GpaphicsRender.EndDraw();
        }
    }
}
