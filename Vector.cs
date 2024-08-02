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
    }
}
