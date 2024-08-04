using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace SharpNEX.Engine.Components
{
    internal class FormManager
    {
        private readonly Game _game;
        private GraphicsRender _imageRender;

        private Form _form;
        private Thread _formThread;

        public FormManager(Game Game, GraphicsRender imageRender, string Title, Size Size)
        {
            _game = Game;
            _imageRender = imageRender;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            _form = new Form();
            _form.Text = Title;
            _form.Size = Size;
            _form.FormBorderStyle = FormBorderStyle.FixedSingle;

            _form.Shown += FormShown;
            _form.MouseDown += FormMouseDown;
            _form.MouseUp += FormMouseUp;
            _form.KeyDown += FormKeyDown;
            _form.KeyUp += FormKeyUp;
        }

        public bool IsShown { get; private set; }

        public void Run()
        {
            Application.Run(_form);
        }

        public void Stop()
        {
            _form.Dispose();
        }

        private void FormShown(object sender, EventArgs e)
        {
            _imageRender.SetForm(_form);
            IsShown = true;
        }

        private void FormMouseDown(object sender, MouseEventArgs e)
        {
            foreach (var gameObject in _game.Scene.GameObjects)
            {
                foreach (var script in gameObject.Scripts)
                {
                    script.OnMouseDown();
                }
            }
        }

        private void FormMouseUp(object sender, MouseEventArgs e)
        {
            foreach (var gameObject in _game.Scene.GameObjects)
            {
                foreach (var script in gameObject.Scripts)
                {
                    script.OnMouseUp();
                }
            }
        }

        private void FormKeyDown(object sender, KeyEventArgs e)
        {
            foreach (var gameObject in _game.Scene.GameObjects)
            {
                foreach (var script in gameObject.Scripts)
                {
                    script.OnKeyDown(e);
                }
            }
        }

        private void FormKeyUp(object sender, KeyEventArgs e)
        {
            foreach (var gameObject in _game.Scene.GameObjects)
            {
                foreach (var script in gameObject.Scripts)
                {
                    script.OnKeyUp(e);
                }
            }
        }
    }
}