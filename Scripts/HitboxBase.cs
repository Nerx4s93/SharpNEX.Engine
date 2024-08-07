using System;
using System.Collections.Generic;

using SharpDX.Mathematics.Interop;

namespace SharpNEX.Engine.Scripts
{
    public class HitboxBase : Script
    {
        public float HitboxSize;
        public Vector DeltaPosition;

        protected List<Vector> Points = new List<Vector>();

        protected void Draw()
        {
            float angleInRadians = Rotation.Angle * Convert.ToSingle(Math.PI) / 180;

            float sin = (float)Math.Sin(angleInRadians);
            float cos = (float)Math.Cos(angleInRadians);

            float rotatedDeltaX = DeltaPosition.X * cos - DeltaPosition.Y * sin;
            float rotatedDeltaY = DeltaPosition.X * sin + DeltaPosition.Y * cos;

            Vector rotatedDelta = new Vector(rotatedDeltaX, rotatedDeltaY);

            for (int i = 0; i < Points.Count; i++)
            {
                Vector pointStart = Position + Points[i] + rotatedDelta;
                Vector pointEnd = Position + (i == Points.Count - 1 ? Points[0] : Points[i + 1]) + rotatedDelta;
                Vector center = Position + rotatedDelta;

                Game.GpaphicsRender.DrawLine(pointStart, pointEnd, 1f, new RawColor4(0, 1, 0, 1), Rotation.Angle, center);
            }
        }
    }
}
