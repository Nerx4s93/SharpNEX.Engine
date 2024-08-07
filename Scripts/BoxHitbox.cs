namespace SharpNEX.Engine.Scripts
{
    public class BoxHitbox : HitboxBase
    {
        public override void Start()
        {
            Points.Add(new Vector(0, 0));
            Points.Add(new Vector(HitboxSize, 0));
            Points.Add(new Vector(HitboxSize, HitboxSize));
            Points.Add(new Vector(0, HitboxSize));
        }

        public override void Update()
        {
            Draw();
        }
    }
}
