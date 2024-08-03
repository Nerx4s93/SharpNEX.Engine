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

            Game.Render(Image, GameObject.Position, GameObject.Rotation);
            GameObject.Rotation = new Quartion(GameObject.Rotation.Angle + 0.05f);
        }
    }
}
