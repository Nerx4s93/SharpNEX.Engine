using SharpDX;

namespace SharpNEX.Engine.Utils
{
    internal static class MatrixBilder
    {
        public static Matrix3x2 Bild(Vector position, Vector size, Vector center, float angle)
        {
            float angleInRadians = Trigonometry.AngleDegreesToRadians(angle);

            var translationToOrigin = Matrix3x2.Translation(-center.X, -center.Y);
            var scaleMatrix = Matrix3x2.Scaling(size.X, size.Y);
            var rotationMatrix = Matrix3x2.Rotation(angleInRadians);
            var translationBack = Matrix3x2.Translation(center.X, center.Y);
            var translationToPosition = Matrix3x2.Translation(position.X - center.X, position.Y - center.Y);

            var combinedMatrix = translationToOrigin * scaleMatrix * rotationMatrix * translationBack * translationToPosition;
            return combinedMatrix;
        }
    }
}
