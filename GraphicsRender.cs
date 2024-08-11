using System;
using System.Windows.Forms;

using SharpNEX.Engine.Utils;
using SharpNEX.Engine.Components;

using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

namespace SharpNEX.Engine
{
    public static class GraphicsRender
    {
        #region CacheManager

        private static CacheManager<string, Bitmap> _bitmapCacheManager = new CacheManager<string, Bitmap>(LoadImage);

        private static Bitmap LoadImage(string imagePath)
        {
            var imagingFactory = new SharpDX.WIC.ImagingFactory();

            var bitmapDecoder = new SharpDX.WIC.BitmapDecoder(imagingFactory, imagePath, SharpDX.WIC.DecodeOptions.CacheOnLoad);
            var frame = bitmapDecoder.GetFrame(0);

            var converter = new SharpDX.WIC.FormatConverter(imagingFactory);
            converter.Initialize(frame, SharpDX.WIC.PixelFormat.Format32bppPBGRA);

            var result = Bitmap.FromWicBitmap(_renderTarget, converter);

            return result;
        }

        #endregion

        private static WindowRenderTarget _renderTarget;

        public static void SetForm(Form form)
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
                PixelSize = new Size2(width, height),
                PresentOptions = PresentOptions.None
            };

            _renderTarget = new WindowRenderTarget(factory, renderProps, hwndRenderTargetProps);
        }

        public static void DrawLine(Vector startPoint, Vector endPoint, float strokeWidth, RawColor4 color)
        {
            using (var brush = new SolidColorBrush(_renderTarget, color))
            {
                _renderTarget.DrawLine(
                    new RawVector2(startPoint.X, startPoint.Y),
                    new RawVector2(endPoint.X, endPoint.Y),
                    brush,
                    strokeWidth
                );
            }
        }

        public static void DrawLine(Vector startPoint, Vector endPoint, float strokeWidth, RawColor4 color, float angle, Vector center)
        {
            Vector newStartPoint = Trigonometry.RotateVector(startPoint, angle, center);
            Vector newEndPoint = Trigonometry.RotateVector(endPoint, angle, center);

            using (var brush = new SolidColorBrush(_renderTarget, color))
            {
                _renderTarget.DrawLine(
                    new RawVector2(newStartPoint.X, newStartPoint.Y),
                    new RawVector2(newEndPoint.X, newEndPoint.Y),
                    brush,
                    strokeWidth
                );
            }
        }

        public static void DrawImage(string imagePath, Vector position, Vector size, float angle)
        {
            Bitmap bitmap = _bitmapCacheManager.GetValue(imagePath);

            var transformMatrix = _renderTarget.Transform;

            float x = bitmap.Size.Width / 2;
            float y = bitmap.Size.Height / 2;
            Vector center = new Vector(x, y);

            var combinedMatrix = MatrixBilder.Bild(position, size, center, angle);

            _renderTarget.Transform = combinedMatrix;

            _renderTarget.DrawBitmap(bitmap, 1.0f, BitmapInterpolationMode.Linear);

            _renderTarget.Transform = transformMatrix;
        }

        internal static void BeginDraw() => _renderTarget.BeginDraw();

        internal static void EndDraw() => _renderTarget.EndDraw();

        internal static void Clear()
        {
            _renderTarget.Clear(new RawColor4(1.0f, 1.0f, 1.0f, 1.0f));
        }

        internal static void Dispose()
        {
            _renderTarget.Dispose();
        }
    }
}
