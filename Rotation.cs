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

        public static Rotation operator +(Rotation a, Rotation b)
        {
            return new Rotation(a.Angle + b.Angle);
        }

        public static Rotation operator -(Rotation a, Rotation b)
        {
            return new Rotation(a.Angle - b.Angle);
        }
    }
}
