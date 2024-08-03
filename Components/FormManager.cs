using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace SharpNEX.Engine.Components
{
    internal class FormManager
    {
        private readonly Game _game;
        private ImageRender _imageRender;

        private Form _form;
        private Thread _formThread;

        public FormManager(Game Game, ImageRender imageRender, string Title, Size Size)
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
    }
}