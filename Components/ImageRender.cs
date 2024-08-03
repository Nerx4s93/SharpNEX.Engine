using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

namespace SharpNEX.Engine.Components
{
    internal class ImageRender
    {
        private WindowRenderTarget _renderTarget;

        private Dictionary<string, Bitmap> _bitmapCache;

        public void SetForm(Form form)
        {
            IntPtr hwnd = form.Handle;
            int width = form.Width; int height = form.Height;

            var factory = new Factory();
            var renderProps = new RenderTargetProperties(
                new PixelFormat(SharpDX.DXGI.Format.B8G8R8A8_UNorm, AlphaMode.Premultiplied)
            );
            var hwndRenderTargetProps = new HwndRenderTargetProperties()
            {
                Hwnd = hwnd,
                PixelSize = new SharpDX.Size2(width, height),
                PresentOptions = PresentOptions.None
            };

            _renderTarget = new WindowRenderTarget(factory, renderProps, hwndRenderTargetProps);
            _bitmapCache = new Dictionary<string, Bitmap>();
        }

        internal void BeginDraw() => _renderTarget.BeginDraw();

        internal void EndDraw() => _renderTarget.EndDraw();

        internal void Clear()
        {
            _renderTarget.Clear(new RawColor4(1.0f, 1.0f, 1.0f, 1.0f));
        }

        public void Render(string imagePath)
        {
            LoadImage(imagePath);
            _renderTarget.DrawBitmap(_bitmapCache[imagePath], 1.0f, BitmapInterpolationMode.Linear);
        }

        internal void Dispose()
        {
            _renderTarget.Dispose();
        }

        private void LoadImage(string imagePath)
        {
            if (_bitmapCache.ContainsKey(imagePath))
            {
                return;
            }

            var imagingFactory = new SharpDX.WIC.ImagingFactory();

            var bitmapDecoder = new SharpDX.WIC.BitmapDecoder(imagingFactory, imagePath, SharpDX.WIC.DecodeOptions.CacheOnLoad);
            var frame = bitmapDecoder.GetFrame(0);

            var converter = new SharpDX.WIC.FormatConverter(imagingFactory);
            converter.Initialize(frame, SharpDX.WIC.PixelFormat.Format32bppPBGRA);

            _bitmapCache.Add(imagePath, Bitmap.FromWicBitmap(_renderTarget, converter));
        }
    }
}
