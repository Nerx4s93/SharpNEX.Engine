namespace SharpNEX.Engine.CacheData
{
    internal class Angle
    {
        public Angle(float radians, float sin, float cos)
        {
            Radians = radians;
            Sin = sin;
            Cos = cos;
        }

        public readonly float Radians;
        public readonly float Sin;
        public readonly float Cos;
    }
}
