﻿namespace SharpNEX.Engine.Scripts
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

            if (newRotation.Angle == 0)
            {
                Game.GpaphicsRender.DrawImage(Image, newPosition, Size);
            }
            else
            {
                Game.GpaphicsRender.DrawImage(Image, newPosition, Size, newRotation.Angle);
            }
        }
    }
}
