using System;
using System.Windows.Forms;

namespace SharpNEX.Engine
{
    [Serializable]
    public class Script
    {
        public GameObject GameObject { get; internal set; }

        public string Name
        {
            get => GameObject.Name;
            set => GameObject.Name = value;
        }

        public GameObject Parent
        {
            get => GameObject.Parent;
        }

        public Vector Position
        {
            get => GameObject.Position;
            set => GameObject.Position = value;
        }

        public Rotation Rotation
        {
            get => GameObject.Rotation;
            set => GameObject.Rotation = value;
        }

        public Vector Size
        {
            get => GameObject.Size;
            set => GameObject.Size = value;
        }

        public bool IsScriptStarted { get; internal set; }

        public void Instante(Script script)
        {
            GameObject.Instante(script);
        }

        public void Instante(GameObject gameObject)
        {
            Game.Scene.Instante(gameObject);
        }

        public void Instante(GameObject gameObject, GameObject parent)
        {
            Game.Scene.Instante(gameObject, parent);
        }

        public void Destroy()
        {
            Game.Scene.Destroy(this);
        }

        public void Destroy(Script script)
        {
            Game.Scene.Destroy(script);
        }

        public void Destroy(GameObject gameObject)
        {
            Game.Scene.Destroy(gameObject);
        }

        public virtual void Start() { }
        public virtual void Update() { }

        public virtual void OnMouseDown(MouseEventArgs e) { }
        public virtual void OnMouseUp(MouseEventArgs e) { }
        public virtual void OnKeyDown(KeyEventArgs e) { }
        public virtual void OnKeyUp(KeyEventArgs e) { }

        public virtual void OnCollision(GameObject gameObject) { }
        public virtual void OnTriggerEnter(GameObject gameObject) { }
        public virtual void OnTriggerLeave(GameObject gameObject) { }
    }
}
