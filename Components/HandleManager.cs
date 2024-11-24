using System;
using System.Drawing;
using System.Windows.Forms;

namespace SharpNEX.Engine.Components
{
    internal class HandleManager
    {
        private IntPtr _handle;
        private Size _size;

        public HandleManager(IntPtr handle, Size size)
        {
            _handle = handle;
            _size = size;
        }

        public bool IsRuned { get; private set; }

        public void Run()
        {
            GraphicsRender.SetForm(_handle, _size);

            /*
 _form.MouseDown += FormMouseDown;
 _form.MouseUp += FormMouseUp;
 _form.KeyDown += FormKeyDown;
 _form.KeyUp += FormKeyUp;*/

            IsRuned = true;
        }

        private void FormMouseDown(object sender, MouseEventArgs e)
        {
            var gameObjects = Game.Scene.GetGameObjects();
            foreach (var gameObject in gameObjects)
            {
                var scripts = gameObject.GetScripts();

                foreach (var script in scripts)
                {
                    script.OnMouseDown(e);
                }
            }
        }

        private void FormMouseUp(object sender, MouseEventArgs e)
        {
            var gameObjects = Game.Scene.GetGameObjects();
            foreach (var gameObject in gameObjects)
            {
                var scripts = gameObject.GetScripts();

                foreach (var script in scripts)
                {
                    script.OnMouseUp(e);
                }
            }
        }

        private void FormKeyDown(object sender, KeyEventArgs e)
        {
            var gameObjects = Game.Scene.GetGameObjects();
            foreach (var gameObject in gameObjects)
            {
                var scripts = gameObject.GetScripts();

                foreach (var script in scripts)
                {
                    script.OnKeyDown(e);
                }
            }
        }

        private void FormKeyUp(object sender, KeyEventArgs e)
        {
            var gameObjects = Game.Scene.GetGameObjects();
            foreach (var gameObject in gameObjects)
            {
                var scripts = gameObject.GetScripts();

                foreach (var script in scripts)
                {
                    script.OnKeyUp(e);
                }
            }
        }
    }
}