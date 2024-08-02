using System.Drawing;

namespace SharpNEX.Engine.Components
{
    internal class DoubleBufferedControl
    {
        private Image _image;
        private Color _clearColor;

        public DoubleBufferedControl(Size formSize, Color clearColor)
        {
            _image = new Bitmap(formSize.Width, formSize.Height);
            Graphics = Graphics.FromImage(_image);
            _clearColor = clearColor;
        }

        public Graphics Graphics;

        public void Clear()
        {
            Graphics.Clear(_clearColor);
        }

        public void Draw(Graphics graphics)
        {
            graphics.DrawImage(_image, 0, 0);
        }
    }
}
