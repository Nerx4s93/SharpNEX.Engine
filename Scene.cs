using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SharpNEX.Engine
{
    public class Scene
    {
        private readonly List<GameObject> _gameObjects;

        private readonly List<GameObject> _loadGameObjects = new List<GameObject>();
        private readonly List<Script> _loadScripts = new List<Script>();
        private readonly List<GameObject> _destroyGameObjects = new List<GameObject>();
        private readonly List<Script> _destroyScripts = new List<Script>();

        public Scene(string Name)
        {
            this.Name = Name;
            _gameObjects = new List<GameObject>();
        }

        public Scene(string Name, List<GameObject> GameObjects)
        {
            this.Name = Name;
            _gameObjects = GameObjects;
        }

        public string Name;

        public ReadOnlyCollection<GameObject> GetGameObjects()
        {
            return _gameObjects.AsReadOnly();
        }

        public void Instantiate(Script script, GameObject gameObject)
        {
            script.GameObject = gameObject;
            _loadScripts.Add(script);

            if (!Game.IsGameRun)
            {
                Instante();
            }
        }

        public GameObject Instantiate(GameObject gameObject)
        {
            var copyGameObject = gameObject.Copy();
            var tree = gameObject.GetAllTree();

            _loadGameObjects.Add(copyGameObject);
            _loadGameObjects.AddRange(tree);

            if (!Game.IsGameRun)
            {
                Instante();
            }

            return copyGameObject;
        }

        public GameObject Instantiate(GameObject gameObject, GameObject parent)
        {
            var copyGameObject = gameObject.Copy();
            var tree = gameObject.GetAllTree();

            copyGameObject.SetParent(parent);

            _loadGameObjects.Add(copyGameObject);
            _loadGameObjects.AddRange(tree);

            if (!Game.IsGameRun)
            {
                Instante();
            }

            return copyGameObject;
        }

        public void Destroy(Script script)
        {
            _destroyScripts.Add(script);

            if (!Game.IsGameRun)
            {
                Destroy();
            }
        }

        public void Destroy(GameObject gameObject)
        {
            var destroy = gameObject.GetAllTree();

            _destroyGameObjects.Add(gameObject);
            _destroyGameObjects.AddRange(destroy);

            if (!Game.IsGameRun)
            {
                Destroy();
            }
        }

        internal void Update()
        {
            Instante();
            Destroy();

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

        private void Destroy()
        {
            foreach (var gameObject in _destroyGameObjects)
            {
                _gameObjects.Remove(gameObject);
            }

            foreach (var script in _destroyScripts)
            {
                var gameObject = script.GameObject;
                gameObject?.RemoveScript(script);
            }
        }
    }
}
