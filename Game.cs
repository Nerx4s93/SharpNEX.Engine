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
        private ImageRender _imageRender;
        private DoubleBufferedControl _doubleBufferedControl;

        private Size _formSize;

        private Thread _gameThread;

        public Game(string Name, Scene Scene, Size Size)
        {
            _name = Name;
            this.Scene = Scene;
            _formSize = Size;
        }

        public static Graphics FormGraphics;

        public Scene Scene;

        public void Run()
        {
            _imageRender = new ImageRender();
            _doubleBufferedControl = new DoubleBufferedControl(_formSize, Color.White);
            _formManager = new FormManager(this, _imageRender, _name, _formSize);

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
            FormGraphics = _doubleBufferedControl.Graphics;

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
