using System;
using System.Collections.Generic;

using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

namespace SharpNEX.Engine.Components
{
    internal class ImageRender
    {
        private readonly WindowRenderTarget renderTarget;

        private Dictionary<string, Bitmap> bitmapCache;

        public ImageRender(IntPtr hwnd, int width, int height)
        {
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

            renderTarget = new WindowRenderTarget(factory, renderProps, hwndRenderTargetProps);
            bitmapCache = new Dictionary<string, Bitmap>();
        }

        public void BeginDraw() => renderTarget.BeginDraw();

        public void EndDraw() => renderTarget.EndDraw();

        public void Clear()
        {
            renderTarget.Clear(new RawColor4(1.0f, 1.0f, 1.0f, 1.0f));
        }

        public void Render(string imagePath)
        {
            LoadImage(imagePath);
            renderTarget.DrawBitmap(bitmapCache[imagePath], 1.0f, BitmapInterpolationMode.Linear);
        }

        public void Dispose()
        {
            renderTarget.Dispose();
        }

        private void LoadImage(string imagePath)
        {
            if (bitmapCache.ContainsKey(imagePath))
            {
                return;
            }

            var imagingFactory = new SharpDX.WIC.ImagingFactory();

            var bitmapDecoder = new SharpDX.WIC.BitmapDecoder(imagingFactory, imagePath, SharpDX.WIC.DecodeOptions.CacheOnLoad);
            var frame = bitmapDecoder.GetFrame(0);

            var converter = new SharpDX.WIC.FormatConverter(imagingFactory);
            converter.Initialize(frame, SharpDX.WIC.PixelFormat.Format32bppPBGRA);

            bitmapCache.Add(imagePath, Bitmap.FromWicBitmap(renderTarget, converter));
        }
    }
}
