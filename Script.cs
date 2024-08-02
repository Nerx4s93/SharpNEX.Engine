namespace SharpNEX.Engine
{
    public class Script
    {
        public GameObject GameObject { get; internal set; }

        public bool IsScriptStarted { get; internal set; }

        public virtual void Start() { }
        public virtual void Update() { }
    }
}
