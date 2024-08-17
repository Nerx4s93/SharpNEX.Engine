using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SharpNEX.Engine
{
    public class Scene
    {
        private readonly List<GameObject> _gameObjects;
        private readonly List<GameObject> _loadGameObjects;

        public Scene(string Name, List<GameObject> GameObjects)
        {
            this.Name = Name;
            _gameObjects = GameObjects;
            _loadGameObjects = new List<GameObject>();
        }

        public string Name;

        public ReadOnlyCollection<GameObject> GetGameObjects()
        {
            return _gameObjects.AsReadOnly();
        }

        public void Instante(GameObject gameObject)
        {
            _loadGameObjects.Add(gameObject);
        }

        public void Instante(GameObject gameObject, GameObject parent)
        {
            gameObject.SetParent(parent);
            _loadGameObjects.Add(gameObject);
        }

        internal void Update()
        {
            _gameObjects.AddRange(_loadGameObjects);
            _loadGameObjects.Clear();

            foreach (var gameObject in _gameObjects)
            {
                var scripts = gameObject.GetScripts();

                foreach (var script in scripts)
                {
                    if (script.IsScriptStarted)
                    {
                        continue;
                    }

                    script.GameObject = gameObject;
                    script.Start();
                    script.IsScriptStarted = true;
                }
            }

            foreach (var gameObject in _gameObjects)
            {
                var scripts = gameObject.GetScripts();

                foreach (var script in scripts)
                {
                    script.Update();
                }
            }
        }
    }
}
