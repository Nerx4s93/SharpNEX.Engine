﻿using System;
using System.Collections.Generic;

using SharpNEX.Engine.CacheData;

namespace SharpNEX.Engine.Utils
{
    internal static class Trigonometry
    {
        private static readonly Dictionary<float, Angle> _angleCache = new Dictionary<float, Angle>();

        public static Angle GetTrigonometricValues(float angleInDegrees)
        {
            bool exits = _angleCache.TryGetValue(angleInDegrees, out var values);

            if (!exits)
            {
                float angleInRadians = AngleDegreesToRadians(angleInDegrees);
                float sin = Convert.ToSingle(Math.Sin(angleInRadians));
                float cos = Convert.ToSingle(Math.Cos(angleInRadians));

                values = new Angle(angleInRadians, sin, cos);

                _angleCache[angleInDegrees] = values;
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