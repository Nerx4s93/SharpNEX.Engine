using System.Collections.Generic;

namespace SharpNEX.Engine
{
    public class GameObject
    {
        public GameObject(string Name, List<Script> Scripts)
        {
            this.Name = Name;
            this.Scripts = Scripts;
        }

        public string Name;
        public List<Script> Scripts;

        public Vector Position;
        public Quartion Rotation;
    }
}
