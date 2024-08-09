namespace SharpNEX.Engine.Scripts
{
    public class BoxHitbox : HitboxBase
    {
        private float _hitboxSizeX;
        private float _hitboxSizeY;

        public float HitboxSizeX
        {
            get
            {
                return _hitboxSizeX;
            }
            set
            {
                _hitboxSizeX = value;
                UpdateHitbox();
            }
        }

        public float HitboxSizeY
        {
            get
            {
                return _hitboxSizeY;
            }
            set
            {
                _hitboxSizeY = value;
                UpdateHitbox();
            }
        }

        public override void Update()
        {
            Draw();
        }

        private void UpdateHitbox()
        {
            Points.Clear();

            float halfHitboxSizeX = _hitboxSizeX / 2;
            float halfHitboxSizeY = _hitboxSizeY / 2;

            Points.Add(new Vector(-halfHitboxSizeX, -halfHitboxSizeY));
            Points.Add(new Vector(halfHitboxSizeX, -halfHitboxSizeY));
            Points.Add(new Vector(halfHitboxSizeX, halfHitboxSizeY));
            Points.Add(new Vector(-halfHitboxSizeX, halfHitboxSizeY));
        }
    }
}
