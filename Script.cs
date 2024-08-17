﻿using System.Windows.Forms;

namespace SharpNEX.Engine
{
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
            GameObject.Instante(gameObject);
        }

        public void Instante(GameObject gameObject, GameObject parent)
        {
            GameObject.Instante(gameObject, parent);
        }

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
