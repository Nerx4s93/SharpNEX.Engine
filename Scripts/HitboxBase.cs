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
            var rotatedDelta = Trigonometry.RotateVector(DeltaPosition, Rotation.Angle);

            var camera = Camera.GetCamera(Camera.СurrentCamera);

            for (int i = 0; i < Points.Count; i++)
            {
                var pointStart = Position + Points[i] + rotatedDelta + camera.Position;
                var pointEnd = Position + (i == Points.Count - 1 ? Points[0] : Points[i + 1]) + rotatedDelta - camera.Position;
                var center = Position + rotatedDelta + camera.Position;

                GraphicsRender.DrawLine(pointStart, pointEnd, 1f, new RawColor4(0, 1, 0, 1), Rotation.Angle, center);
            }
        }
    }
}
