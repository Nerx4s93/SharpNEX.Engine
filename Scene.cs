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
                foreach (var script in gameObject.Scripts)
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
                foreach (var script in gameObject.Scripts)
                {
                    script.Update();
                }
            }
        }
    }
}
