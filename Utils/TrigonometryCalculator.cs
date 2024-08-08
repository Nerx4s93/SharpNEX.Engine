using System;
using System.Collections.Generic;

using SharpNEX.Engine.Cache;

namespace SharpNEX.Engine.Utils
{
    internal static class TrigonometryCalculator
    {
        private static readonly Dictionary<float, Angle> angleCache = new Dictionary<float, Angle>();

        private static Angle GetTrigonometricValues(float angleInDegrees)
        {
            bool exits = angleCache.TryGetValue(angleInDegrees, out var values);

            if (!exits)
            {
                float angleInRadians = AngleDegreesToRadians(angleInDegrees);
                float sin = Convert.ToSingle(Math.Sin(angleInRadians));
                float cos = Convert.ToSingle(Math.Cos(angleInRadians));

                values = new Angle(angleInRadians, sin, cos);

                angleCache[angleInDegrees] = values;
                Console.WriteLine(angleInDegrees);
            }

            return values;
        }

        public static float AngleDegreesToRadians(float angleInDegrees)
        {
            float angleInRadians = angleInDegrees * Convert.ToSingle(Math.PI) / 180;
            return angleInRadians;
        }

        public static Vector RotateVector(Vector vector, float angleInDegrees)
        {
            Angle angle = GetTrigonometricValues(angleInDegrees);

            float x = vector.X * angle.Cos - vector.Y * angle.Sin;
            float y = vector.X * angle.Sin + vector.Y * angle.Cos;

            Vector result = new Vector(x, y);
            return result;
        }

        public static Vector RotateVector(Vector vector, float angleInDegrees, Vector center)
        {
            Angle angle = GetTrigonometricValues(angleInDegrees);

            float x = center.X + (vector.X - center.X) * angle.Cos - (vector.Y - center.Y) * angle.Sin;
            float y = center.Y + (vector.X - center.X) * angle.Sin + (vector.Y - center.Y) * angle.Cos;

            Vector result = new Vector(x, y);
            return result;
        }
    }
}
