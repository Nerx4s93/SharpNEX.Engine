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
        private static ImageRender _imageRender;

        private Size _formSize;

        private Thread _gameThread;

        public Game(string Name, Scene Scene, Size Size)
        {
            _name = Name;
            this.Scene = Scene;
            _formSize = Size;
        }

        public Scene Scene;

        public static void Render(string imagePath, Vector position, Quartion rotation) => _imageRender.Render(imagePath, position, rotation);
        public static void Render(string imagePath, Vector position) => _imageRender.Render(imagePath, position);

        public void Run()
        {
            _imageRender = new ImageRender();
            _formManager = new FormManager(this, _imageRender, _name, _formSize);

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
            _imageRender.BeginDraw();
            _imageRender.Clear();

            Scene.Update();

            _imageRender.EndDraw();
        }
    }
}
