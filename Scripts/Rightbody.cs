using System;
using System.Collections.Generic;

using SharpNEX.Engine.Utils;

#pragma warning disable CS0168

namespace SharpNEX.Engine.Scripts
{
    public class Rightbody : Script
    {
        private HitboxBase _hitboxBase;
        private Vector _velocity;

        public float Friction = 10000;
        public float Weight = 200;

        public Vector Velocity
        {
            get
            {
                return _velocity;
            }
            private set
            {
                _velocity = value;
            }
        }

        public void AddForce(Vector velocity)
        {
            Velocity += velocity;
        }

        public override void Start()
        {
            _hitboxBase = GameObject.GetScriptFromBaseType<HitboxBase>();
        }

        public override void Update()
        {
            ForceMove();

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

                    bool colision = Physics.ColisionHitboxes(_hitboxBase, hitboxBase);

                    if (colision && !hitboxBase.IsTrigger && !_hitboxBase.IsTrigger)
                    {
                        Physics.RepellingObjects(GameObject, gameObject);
                    }
                }
                catch (InvalidOperationException invalidOperationException) { }
            }
        }

        private void ForceMove()
        {
            Vector move = Physics.DistanceTraveled(Friction, Weight, 9.8f, ref _velocity);
            Position += move;
        }
    }
}
