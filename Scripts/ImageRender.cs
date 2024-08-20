using System;

namespace SharpNEX.Engine.Scripts
{
    [Serializable]
    public class ImageRender : Script
    {
        public string Image;

        public Vector DeltaPosition;
        public Rotation DeltaRotation;

        public override void Update()
        {
            if (string.IsNullOrEmpty(Image))
            {
                return;
            }

            var camera = Camera.GetCamera(Camera.СurrentCamera);

            var newPosition = Position + DeltaPosition - camera.Position;
            var newRotation = Rotation + DeltaRotation;

            GraphicsRender.DrawImage(Image, newPosition, Size, newRotation.Angle);
        }
    }
}
