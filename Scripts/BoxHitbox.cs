namespace SharpNEX.Engine.Scripts
{
    public class BoxHitbox : HitboxBase
    {
        public override void Start()
        {
            float halfHitboxSize = HitboxSize / 2;

            Points.Add(new Vector(-halfHitboxSize, -halfHitboxSize));
            Points.Add(new Vector(halfHitboxSize, -halfHitboxSize));
            Points.Add(new Vector(halfHitboxSize, halfHitboxSize));
            Points.Add(new Vector(-halfHitboxSize, halfHitboxSize));
        }

        public override void Update()
        {
            Draw();
        }
    }
}
