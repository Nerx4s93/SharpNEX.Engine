using System;
using System.Collections.Generic;
using System.Windows.Forms;

using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

namespace SharpNEX.Engine.Components
{
    public class GraphicsRender
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
                PixelSize = new Size2(width, height),
                PresentOptions = PresentOptions.None
            };

            _renderTarget = new WindowRenderTarget(factory, renderProps, hwndRenderTargetProps);
            _bitmapCache = new Dictionary<string, Bitmap>();
        }

        public void DrawLine(Vector startPoint, Vector endPoint, float strokeWidth, RawColor4 color)
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

        public void DrawLine(Vector startPoint, Vector endPoint, float strokeWidth, RawColor4 color, float angle, Vector center)
        {
            float angleInRadians = angle * Convert.ToSingle(Math.PI) / 180;

            float cos = Convert.ToSingle(Math.Cos(angleInRadians));
            float sin = Convert.ToSingle(Math.Sin(angleInRadians));

            float x1 = center.X + (startPoint.X - center.X) * cos - (startPoint.Y - center.Y) * sin;
            float y1 = center.Y + (startPoint.X - center.X) * sin + (startPoint.Y - center.Y) * cos;
            float x2 = center.X + (endPoint.X - center.X) * cos - (endPoint.Y - center.Y) * sin;
            float y2 = center.Y + (endPoint.X - center.X) * sin + (endPoint.Y - center.Y) * cos;

            using (var brush = new SolidColorBrush(_renderTarget, color))
            {
                _renderTarget.DrawLine(
                    new RawVector2(x1, y1),
                    new RawVector2(x2, y2),
                    brush,
                    strokeWidth
                );
            }
        }

        public void DrawImage(string imagePath, Vector position, Vector size)
        {
            LoadImage(imagePath);

            Bitmap bitmap = _bitmapCache[imagePath];

            var transformMatrix = _renderTarget.Transform;

            var translationToPosition = Matrix3x2.Translation(position.X, position.Y);
            var scaleMatrix = Matrix3x2.Scaling(size.X, size.Y);

            _renderTarget.Transform = scaleMatrix * translationToPosition;

            _renderTarget.DrawBitmap(bitmap, 1.0f, BitmapInterpolationMode.Linear);

            _renderTarget.Transform = transformMatrix;
        }

        public void DrawImage(string imagePath, Vector position, Vector size, float angle)
        {
            LoadImage(imagePath);

            Bitmap bitmap = _bitmapCache[imagePath];

            float angleInRadians = angle * Convert.ToSingle(Math.PI) / 180;

            float x = bitmap.Size.Width / 2;
            float y = bitmap.Size.Height / 2;
            var transformMatrix = _renderTarget.Transform;

            var translationToOrigin = Matrix3x2.Translation(-x, -y);
            var scaleMatrix = Matrix3x2.Scaling(size.X, size.Y);
            var rotationMatrix = Matrix3x2.Rotation(angleInRadians);
            var translationBack = Matrix3x2.Translation(x, y);
            var translationToPosition = Matrix3x2.Translation(position.X - x, position.Y - y);

            var combinedMatrix = translationToOrigin * scaleMatrix * rotationMatrix * translationBack * translationToPosition;

            _renderTarget.Transform = combinedMatrix;

            _renderTarget.DrawBitmap(bitmap, 1.0f, BitmapInterpolationMode.Linear);

            _renderTarget.Transform = transformMatrix;
        }

        internal void BeginDraw() => _renderTarget.BeginDraw();

        internal void EndDraw() => _renderTarget.EndDraw();

        internal void Clear()
        {
            _renderTarget.Clear(new RawColor4(1.0f, 1.0f, 1.0f, 1.0f));
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
