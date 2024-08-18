using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SharpNEX.Engine
{
    public class Scene
    {
        private readonly List<GameObject> _gameObjects;

        private readonly List<GameObject> _loadGameObjects;
        private readonly List<Script> _loadScripts;

        public Scene(string Name)
        {
            this.Name = Name;
            _gameObjects = new List<GameObject>();
            _loadGameObjects = new List<GameObject>();
            _loadScripts = new List<Script>();
        }

        public Scene(string Name, List<GameObject> GameObjects)
        {
            this.Name = Name;
            _gameObjects = GameObjects;
            _loadGameObjects = new List<GameObject>();
            _loadScripts = new List<Script>();
        }

        public string Name;

        public ReadOnlyCollection<GameObject> GetGameObjects()
        {
            return _gameObjects.AsReadOnly();
        }

        public void Instante(Script script, GameObject gameObject)
        {
            script.GameObject = gameObject;
            _loadScripts.Add(script);
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
            Instante();

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

        private void Instante()
        {
            _gameObjects.AddRange(_loadGameObjects);
            _loadGameObjects.Clear();

            foreach (var script in _loadScripts)
            {
                var gameObject = script.GameObject;
                gameObject.AddScript(script);
            }
            _loadScripts.Clear();
        }
    }
}
