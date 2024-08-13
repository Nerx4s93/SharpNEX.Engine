using System.Collections.Generic;

namespace SharpNEX.Engine
{
    public class Scene
    {
        public Scene(string Name, List<GameObject> GameObjects)
        {
            this.Name = Name;
            this.GameObjects = GameObjects;
        }

        public string Name;
        public List<GameObject> GameObjects;

        internal void Update()
        {
            foreach (var gameObject in GameObjects)
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

            foreach (var gameObject in GameObjects)
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
