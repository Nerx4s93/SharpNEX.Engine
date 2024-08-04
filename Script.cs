using System.Windows.Forms;

namespace SharpNEX.Engine
{
    public class Script
    {
        public GameObject GameObject { get; internal set; }

        public Vector Position => GameObject.Position;
        public Quartion Rotation => GameObject.Rotation;
        public Vector Size => GameObject.Size;

        public bool IsScriptStarted { get; internal set; }

        public virtual void Start() { }
        public virtual void Update() { }

        public virtual void OnMouseDown() { }
        public virtual void OnMouseUp() { }
        public virtual void OnKeyDown(KeyEventArgs e) { }
        public virtual void OnKeyUp(KeyEventArgs e) { }
    }
}
