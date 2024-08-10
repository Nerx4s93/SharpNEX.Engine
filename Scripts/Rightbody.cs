using System;
using System.Collections.Generic;

using SharpNEX.Engine.Utils;

#pragma warning disable CS0168

namespace SharpNEX.Engine.Scripts
{
    public class Rightbody : Script
    {
        private HitboxBase _hitboxBase;

        public override void Start()
        {
            _hitboxBase = GameObject.GetScriptFromBaseType<HitboxBase>();
        }

        public override void Update()
        {
            List<GameObject> gameObjects = Game.Scene.GameObjects;

            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject == GameObject)
                {
                    continue;
                }

                try
                {
                    HitboxBase hitboxBase = gameObject.GetScriptFromBaseType<HitboxBase>();

                    bool colision = CollisionCalculator.ColisionHitboxes(_hitboxBase, hitboxBase);
                    //TODO: отталкивание
                }
                catch (InvalidOperationException invalidOperationException) { }
            }
        }
    }
}
