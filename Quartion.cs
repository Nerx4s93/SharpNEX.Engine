namespace SharpNEX.Engine
{
    public struct Quartion
    {
        public Quartion(float Angle)
        {
            this.Angle = Angle;
        }

        public float Angle;

        public static Quartion operator +(Quartion a, Quartion b)
        {
            return new Quartion(a.Angle + b.Angle);
        }

        public static Quartion operator -(Quartion a, Quartion b)
        {
            return new Quartion(a.Angle - b.Angle);
        }
    }
}
