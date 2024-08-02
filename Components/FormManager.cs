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
        }

        public void Run()
        {
            _formThread = new Thread(() =>
            {
                Application.Run(_form);
            });
            _formThread.Start();
        }

        public void Stop()
        {
            _formThread.Abort();
            _form.Dispose();
        }
    }
}