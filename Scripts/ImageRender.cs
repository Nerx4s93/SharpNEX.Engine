namespace SharpNEX.Engine.Scripts
{
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

            Vector newPosition = Position + DeltaPosition;
            Rotation newRotation = Rotation + DeltaRotation;

            GraphicsRender.DrawImage(Image, newPosition, Size, newRotation.Angle);
        }
    }
}
