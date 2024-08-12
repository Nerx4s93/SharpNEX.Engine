using System.Collections.Generic;

using SharpNEX.Engine.Utils;

using SharpDX.Mathematics.Interop;

namespace SharpNEX.Engine.Scripts
{
    public class HitboxBase : Script
    {
        public bool IsTrigger;
        public Vector DeltaPosition;

        public List<Vector> Points { get; protected set; } = new List<Vector>();

        internal List<GameObject> GameObjectsTriggerEnter = new List<GameObject>();

        protected void Draw()
        {
            Vector rotatedDelta = Trigonometry.RotateVector(DeltaPosition, Rotation.Angle);

            for (int i = 0; i < Points.Count; i++)
            {
                Vector pointStart = Position + Points[i] + rotatedDelta;
                Vector pointEnd = Position + (i == Points.Count - 1 ? Points[0] : Points[i + 1]) + rotatedDelta;
                Vector center = Position + rotatedDelta;

                GraphicsRender.DrawLine(pointStart, pointEnd, 1f, new RawColor4(0, 1, 0, 1), Rotation.Angle, center);
            }
        }
    }
}
