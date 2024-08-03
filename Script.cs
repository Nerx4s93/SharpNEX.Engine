namespace SharpNEX.Engine
{
    public class Script
    {
        public GameObject GameObject { get; internal set; }

        public Vector Position => GameObject.Position;
        public Quartion Rotation => GameObject.Rotation;

        public bool IsScriptStarted { get; internal set; }

        public virtual void Start() { }
        public virtual void Update() { }
    }
}
