using SharpNEX.Engine.Cache;
using SharpNEX.Engine.Cache.Data;

namespace SharpNEX.Engine.EngineMath
{
    internal static class Trigonometry
    {
        #region CacheManager

        private static readonly CacheManager<float, Angle> AngleCacheManager = new CacheManager<float, Angle>(SetAngleCacheValue);

        private static Angle SetAngleCacheValue(float key)
        {
            var angleInRadians = AngleDegreesToRadians(key);
            var sin = Convert.ToSingle(Math.Sin(angleInRadians));
            var cos = Convert.ToSingle(Math.Cos(angleInRadians));

            return new Angle(angleInRadians, sin, cos);
        }

        #endregion

        // Перевод угла из градусов в радианы с округлением ключа
        public static float AngleDegreesToRadians(float angleInDegrees)
        {
            var roundedAngle = (float)Math.Round(angleInDegrees, 3);
            var angleInRadians = roundedAngle * Convert.ToSingle(Math.PI) / 180f;
            return angleInRadians;
        }

        // Повернуть вектор
        public static Vector RotateVector(Vector vector, float angleInDegrees)
        {
            var cacheKey = (float)Math.Round(angleInDegrees, 3);
            var angle = AngleCacheManager.GetValue(cacheKey);

            var x = vector.X * angle.Cos - vector.Y * angle.Sin;
            var y = vector.X * angle.Sin + vector.Y * angle.Cos;

            return new Vector(x, y);
        }

        // Повернуть вектор вокруг заданной точки
        public static Vector RotateVector(Vector vector, float angleInDegrees, Vector center)
        {
            var cacheKey = (float)Math.Round(angleInDegrees, 3);
            var angle = AngleCacheManager.GetValue(cacheKey);

            var x = center.X + (vector.X - center.X) * angle.Cos - (vector.Y - center.Y) * angle.Sin;
            var y = center.Y + (vector.X - center.X) * angle.Sin + (vector.Y - center.Y) * angle.Cos;

            return new Vector(x, y);
        }
    }
}