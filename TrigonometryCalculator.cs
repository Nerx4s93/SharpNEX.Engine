using System;

namespace SharpNEX.Engine
{
    internal static class TrigonometryCalculator
    {
        public static float AngleDegreesToRadians(float angleInDegrees)
        {
            float angleInRadians = angleInDegrees * Convert.ToSingle(Math.PI) / 180;
            return angleInRadians;
        }

        public static Vector RotateVector(Vector vector, float angleInDegrees)
        {
            float angleInRadians = AngleDegreesToRadians(angleInDegrees);

            float cos = Convert.ToSingle(Math.Cos(angleInRadians));
            float sin = Convert.ToSingle(Math.Sin(angleInRadians));

            float x = vector.X * cos - vector.Y * sin;
            float y = vector.X * sin + vector.Y * cos;

            Vector result = new Vector(x, y);
            return result;
        }

        public static Vector RotateVector(Vector vector, float angleInDegrees, Vector center)
        {
            float angleInRadians = AngleDegreesToRadians(angleInDegrees);

            float cos = Convert.ToSingle(Math.Cos(angleInRadians));
            float sin = Convert.ToSingle(Math.Sin(angleInRadians));

            float x = center.X + (vector.X - center.X) * cos - (vector.Y - center.Y) * sin;
            float y = center.Y + (vector.X - center.X) * sin + (vector.Y - center.Y) * cos;

            Vector result = new Vector(x, y);
            return result;
        }
    }
}
