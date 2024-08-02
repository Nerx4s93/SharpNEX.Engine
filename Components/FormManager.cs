using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace SharpNEX.Engine.Components
{
    internal class FormManager
    {
        private readonly Game _game;

        private Form _form;
        private Thread _formThread;

        public FormManager(Game Game, string Title, Size Size)
        {
            _game = Game;

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

        public Graphics GetGraphics()
        {
            return _form.CreateGraphics();
        }

        private void FormShown(object sender, EventArgs e)
        {
            IsShown = true;
        }
    }
}