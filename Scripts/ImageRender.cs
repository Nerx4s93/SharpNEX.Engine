namespace SharpNEX.Engine.Scripts
{
    public class ImageRender : Script
    {
        public string Image;

        public Vector DeltaPosition;
        public Quartion DeltaRotation;

        public override void Update()
        {
            if (string.IsNullOrEmpty(Image))
            {
                return;
            }

            Vector newPosition = Position + DeltaPosition;
            Quartion newRotation = Rotation + DeltaRotation;

            if (newRotation.Angle == 0)
            {
                Game.Render(Image, newPosition);
            }
            else
            {
                Game.Render(Image, newPosition, newRotation);
            }
        }
    }
}
