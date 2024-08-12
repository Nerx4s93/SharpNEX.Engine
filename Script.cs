using System.Windows.Forms;

namespace SharpNEX.Engine
{
    public class Script
    {
        public GameObject GameObject { get; internal set; }

        public Vector Position
        {
            get
            {
                return GameObject.Position;
            }
            set
            {
                GameObject.Position = value;
            }
        }

        public Rotation Rotation
        {
            get
            {
                return GameObject.Rotation;
            }
            set
            {
                GameObject.Rotation = value;
            }
        }

        public Vector Size
        {
            get
            {
                return GameObject.Size;
            }
            set
            {
                GameObject.Size = value;
            }
        }

        public bool IsScriptStarted { get; internal set; }

        public virtual void Start() { }
        public virtual void Update() { }

        public virtual void OnMouseDown() { }
        public virtual void OnMouseUp() { }
        public virtual void OnKeyDown(KeyEventArgs e) { }
        public virtual void OnKeyUp(KeyEventArgs e) { }

        public virtual void OnCollision(GameObject gameObject) { }
        public virtual void OnTriggerEnter(GameObject gameObject) { }
        public virtual void OnTriggerLeave(GameObject gameObject) { }
    }
}
