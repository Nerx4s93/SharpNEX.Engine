using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SharpNEX.Engine
{
    public class Scene
    {
        private List<GameObject> _gameObjects;

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

        internal void Update()
        {
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
