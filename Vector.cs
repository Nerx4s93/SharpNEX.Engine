namespace SharpNEX.Engine
{
    public struct Vector
    {
        public Vector(float X, float Y)
        {
            this.X = X; this.Y = Y;
        }

        public float X;

        public float Y;

        public static Vector Zero = new Vector(0, 0);

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y);
        }

        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.X - b.X, a.Y - b.Y);
        }
    }
}
