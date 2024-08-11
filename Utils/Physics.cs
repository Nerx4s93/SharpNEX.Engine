namespace SharpNEX.Engine.Utils
{
    internal static class Physics
    {
        public static Vector DistanceTraveled(float friction, float weight, float g, ref Vector velocity)
        {
            float force = friction * weight * g;

            float accelerationX = force * velocity.X * 0.000001f / weight;
            float accelerationY = force * velocity.Y * 0.000001f / weight;

            velocity.X -= accelerationX * Game.DeltaTime;
            velocity.Y -= accelerationY * Game.DeltaTime;

            Vector result = new Vector(velocity.X * Game.DeltaTime, velocity.Y * Game.DeltaTime);

            return result;
        }
    }
}