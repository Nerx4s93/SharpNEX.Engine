using System;
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
        public Rotation Rotation;
        public Vector Size = new Vector(1, 1);

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

                if (typeof(T) == baseScriptType)
                {
                    var result = (T)Convert.ChangeType(script, typeof(T));
                    return result;
                }
            }

            throw new InvalidOperationException($"Скрипт \"{typeof(T).Name}\" не наден");
        }
    }
}
