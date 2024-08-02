namespace SharpNEX.Engine
{
    public class Script
    {
        public bool IsScriptStarted { get; internal set; }

        public virtual void Start() { }
        public virtual void Update() { }
    }
}
