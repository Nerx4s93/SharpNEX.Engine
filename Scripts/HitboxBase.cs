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
            Vector rotatedDelta = TrigonometryCalculator.RotateVector(DeltaPosition, Rotation.Angle);

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
