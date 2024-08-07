using System;
using System.Collections.Generic;
using System.Windows.Forms;

using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Direct2D1.Effects;
using SharpDX.Mathematics.Interop;
using static System.Windows.Forms.AxHost;

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
                PixelSize = new SharpDX.Size2(width, height),
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

        public void Render(string imagePath, Vector position, Quartion rotation, Vector size)
        {
            LoadImage(imagePath);

            Bitmap bitmap = _bitmapCache[imagePath];

            float x = position.X + bitmap.Size.Width / 2;
            float y = position.Y + bitmap.Size.Height / 2;
            float angleInRadians = rotation.Angle * Convert.ToSingle(Math.PI) / 180;

            var transformMatrix = _renderTarget.Transform;

            var translationToPosition = Matrix3x2.Translation(position.X, position.Y);
            var translationToOrigin = Matrix3x2.Translation(-x, -y);
            var rotationMatrix = Matrix3x2.Rotation(angleInRadians);
            var translationBack = Matrix3x2.Translation(x, y);
            var scaleMatrix = Matrix3x2.Scaling(size.X, size.Y, new Vector2(x, y));

            var combinedMatrix = translationToPosition * translationToOrigin * rotationMatrix * translationBack * scaleMatrix;

            _renderTarget.Transform = combinedMatrix;

            _renderTarget.DrawBitmap(bitmap, 1.0f, BitmapInterpolationMode.Linear);

            _renderTarget.Transform = transformMatrix;
        }

        public void Render(string imagePath, Vector position, Vector size)
        {
            LoadImage(imagePath);

            Bitmap bitmap = _bitmapCache[imagePath];

            var transformMatrix = _renderTarget.Transform;

            float x = position.X + bitmap.Size.Width / 2;
            float y = position.Y + bitmap.Size.Height / 2;

            var translationToPosition = Matrix3x2.Translation(position.X, position.Y);
            var scaleMatrix = Matrix3x2.Scaling(size.X, size.Y, new Vector2(x, y));

            _renderTarget.Transform = translationToPosition * scaleMatrix;

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
