using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SharpNEX.Engine
{
    public class GameObject
    {
        private readonly List<Script> Scripts;
        private readonly List<GameObject> Childs = new List<GameObject>();
        private Vector _position;
        private Rotation _rotation;
        private Vector _size;

        public GameObject(string Name, List<Script> Scripts)
        {
            this.Name = Name;
            this.Scripts = Scripts;
        }

        public string Name;

        public GameObject Parent { get; private set; }

        public Vector Position
        {
            get
            {
                return _position;
            }
            set
            {
                var delta = value - _position;

                _position = value;
                
                foreach (var gameObjcet in Childs)
                {
                    gameObjcet.Position += delta;
                }
            }
        }

        public Rotation Rotation
        {
            get
            {
                return _rotation;
            }
            set
            {
                var delta = value - _rotation;

                _rotation = value;

                foreach (var gameObjcet in Childs)
                {
                    gameObjcet.Rotation = _rotation;
                }
            }
        }

        public Vector Size
        {
            get
            {
                return _size;
            }
            set
            {
                var delta = value - _size;

                _size = value;

                foreach (var gameObjcet in Childs)
                {
                    gameObjcet.Size += delta;
                }
            }
        }

        public ReadOnlyCollection<Script> GetScripts()
        {
            return Scripts.AsReadOnly();
        }

        public ReadOnlyCollection<GameObject> GetChilds()
        {
            return Childs.AsReadOnly();
        }

        public void AddChild(GameObject gameObject)
        {
            if (gameObject.Parent != this)
            {
                gameObject.Parent?.RemoveChild(gameObject);
                gameObject.Parent = this;
            }
            Childs.Add(gameObject);
        }

        public void RemoveChild(GameObject gameObject)
        {
            if (gameObject.Parent == this)
            {
                gameObject.Parent = null;
            }
            Childs.Remove(gameObject);
        }

        public void SetParent(GameObject gameObject)
        {
            Parent?.RemoveChild(this);
            gameObject.AddChild(this);
        }

        public T GetScript<T>()
        {
            foreach (Script script in Scripts)
            {
                var scriptType = script.GetType();

                if (typeof(T) == scriptType)
                {
                    var result = (T)Convert.ChangeType(script, typeof(T));
                    return result;
                }
            }

            throw new InvalidOperationException($"Скрипт \"{typeof(T).Name}\" не наден");
        }

        public T GetScriptFromBaseType<T>()
        {
            foreach (Script script in Scripts)
            {
                var baseScriptType = script.GetType().BaseType;

                if (typeof(T).IsAssignableFrom(baseScriptType))
                {
                    var result = (T)Convert.ChangeType(script, script.GetType());
                    return result;
                }
            }

            throw new InvalidOperationException($"Скрипт \"{typeof(T).Name}\" не наден");
        }
    }
}
