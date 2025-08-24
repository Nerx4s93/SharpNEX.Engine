using SharpNEX.Engine.Platform;

namespace SharpNEX.Engine.Scripts
{
    public class SpriteRenderer : Script
    {
        public ITexture? Texture { get; set; }

        public override void Update()
        {
            if (Texture == null)
            {
                return;
            }

            var position = GameObject.Position;
            Game.Renderer!.DrawTexture(Texture, position.X, position.Y, Texture.Width, Texture.Height);
        }
    }
}
