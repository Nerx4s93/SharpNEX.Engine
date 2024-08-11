using System;

namespace SharpNEX.Engine
{
    public struct Rotation
    {
        public Rotation(float Angle)
        {
            while (Angle >= 360)
            {
                Angle -= 360;
            }

            this.Angle = Angle;
        }

        public float Angle;

        #region Операторы

        public static Rotation operator +(Rotation a, Rotation b)
        {
            float result = (float)Math.Round(a.Angle + b.Angle, 3);
            return new Rotation(result);
        }

        public static Rotation operator -(Rotation a, Rotation b)
        {
            float result = (float)Math.Round(a.Angle - b.Angle, 3);
            return new Rotation(result);
        }

        public static Rotation operator +(Rotation a, float b)
        {
            float result = (float)Math.Round(a.Angle + b, 3);
            return new Rotation(result);
        }

        public static Rotation operator -(Rotation a, float b)
        {
            float result = (float)Math.Round(a.Angle - b, 3);
            return new Rotation(result);
        }

        #endregion
    }
}
