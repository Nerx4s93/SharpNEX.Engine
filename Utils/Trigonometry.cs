using System;

using SharpNEX.Engine.CacheData;
using SharpNEX.Engine.Components;

namespace SharpNEX.Engine.Utils
{
    internal static class Trigonometry
    {
        #region CacheManager

        private static CacheManager<float, Angle> _angleCacheManager = new CacheManager<float, Angle>(SetAngleCacheValue);

        private static Angle SetAngleCacheValue(float key)
        {
            float angleInRadians = AngleDegreesToRadians(key);
            float sin = Convert.ToSingle(Math.Sin(angleInRadians));
            float cos = Convert.ToSingle(Math.Cos(angleInRadians));

            var result = new Angle(angleInRadians, sin, cos);
            return result;
        }

        #endregion

        // Перевод угла из градусов в радиаты
        public static float AngleDegreesToRadians(float angleInDegrees)
        {
            float angleInRadians = angleInDegrees * Convert.ToSingle(Math.PI) / 180;
            return angleInRadians;
        }

        // Повернуть вектор
        public static Vector RotateVector(Vector vector, float angleInDegrees)
        {
            var angle = _angleCacheManager.GetValue(angleInDegrees);

            float x = vector.X * angle.Cos - vector.Y * angle.Sin;
            float y = vector.X * angle.Sin + vector.Y * angle.Cos;

            var result = new Vector(x, y);
            return result;
        }

        // Повернуть вектор вокруг заданной точки
        public static Vector RotateVector(Vector vector, float angleInDegrees, Vector center)
        {
            var angle = _angleCacheManager.GetValue(angleInDegrees);

            float x = center.X + (vector.X - center.X) * angle.Cos - (vector.Y - center.Y) * angle.Sin;
            float y = center.Y + (vector.X - center.X) * angle.Sin + (vector.Y - center.Y) * angle.Cos;

            var result = new Vector(x, y);
            return result;
        }
    }
}
