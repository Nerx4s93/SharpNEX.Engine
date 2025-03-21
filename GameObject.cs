﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace SharpNEX.Engine
{
    [Serializable]
    public class GameObject
    {
        private readonly List<Script> Scripts;
        private readonly List<GameObject> Childs = new List<GameObject>();

        private Vector _position;
        private Rotation _rotation;
        private Vector _size;

        public GameObject(string Name)
        {
            this.Name = Name;
            Scripts = new List<Script>();
        }

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
                
                foreach (var gameObject in Childs)
                {
                    gameObject.Position += delta;
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

                foreach (var gameObject in Childs)
                {
                    gameObject.Rotation += delta;
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

                foreach (var gameObject in Childs)
                {
                    gameObject.Size += delta;
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
            gameObject?.AddChild(this);
        }

        public bool HasScript<T>()
        {
            foreach (Script script in Scripts)
            {
                var scriptType = script.GetType();

                if (typeof(T) == scriptType)
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasScriptFromBaseType<T>()
        {
            foreach (Script script in Scripts)
            {
                var baseScriptType = script.GetType().BaseType;

                if (typeof(T).IsAssignableFrom(baseScriptType))
                {
                    return true;
                }
            }

            return false;
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

            throw new InvalidOperationException($"Скрипт \"{typeof(T).Name}\" не найден");
        }

        public void Instantiate()
        {
            Game.Scene.Instantiate(this);
        }

        public void Instantiate(Script script)
        {
            Game.Scene.Instantiate(script, this);
        }

        public void Destroy()
        {
            Game.Scene.Destroy(this);
        }

        internal void ClearChild()
        {
            Childs.Clear();
        }

        internal void AddScript(Script script)
        {
            Scripts.Add(script);
        }

        internal void RemoveScript(Script script)
        {
            Scripts.Remove(script);
        }

        internal List<GameObject> GetAllTree()
        {
            var result = new List<GameObject>();
            result.AddRange(Childs);

            foreach (var gameObject in Childs)
            {
                var childsGameObjects = gameObject.GetAllTree();
                result.AddRange(childsGameObjects);
            }

            return result;
        }

        internal List<GameObject> GetAllCopyTree(GameObject copyParent)
        {
            var originalTree = GetAllTree();
            var copyMap = new Dictionary<GameObject, GameObject>();

            foreach (var gameObject in originalTree)
            {
                var copiedGameObject = gameObject.Copy();
                copyMap[gameObject] = copiedGameObject[0];
            }

            foreach (var originalGameObject in originalTree)
            {
                var copiedGameObject = copyMap[originalGameObject];
                var parent = originalGameObject.Parent == this ? copyParent : copyMap[originalGameObject.Parent];

                copiedGameObject.SetParent(parent);
            }

            return copyMap.Values.ToList();
        }

        internal List<GameObject> Copy()
        {
            var gameObject = this;

            using (var memoryStream = new MemoryStream())
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, gameObject);
                memoryStream.Position = 0;

                var result = new List<GameObject>() { binaryFormatter.Deserialize(memoryStream) as GameObject };
                var copyChilds = GetAllCopyTree(result[0]);

                //Отчистка ссылок на оригинальные объекты
                result[0].Childs.Clear();
                result[0].Childs.AddRange(copyChilds);

                result.AddRange(copyChilds);

                return result;
            }
        }
    }
}
