using System;

using SharpNEX.Engine.Utils;

#pragma warning disable CS0168

namespace SharpNEX.Engine.Scripts
{
    [Serializable]
    public class Rightbody : Script
    {
        private HitboxBase _hitboxBase;
        private Vector _velocity;

        public float Elasticity = 0.5f;
        public float Friction = 20000;
        public float Weight = 200;

        public Vector Velocity
        {
            get
            {
                return _velocity;
            }
            set
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

            var gameObjects = Game.Scene.GetGameObjects();
            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject == GameObject)
                {
                    continue;
                }

                HitboxBase hitboxBase;
                try
                {
                    hitboxBase = gameObject.GetScriptFromBaseType<HitboxBase>();
                }
                catch
                {
                    continue;
                }

                bool colision = Physics.ColisionHitboxes(_hitboxBase, hitboxBase);
                var scripts = GameObject.GetScripts();

                if (colision)
                {
                    if (!_hitboxBase.IsTrigger && hitboxBase.IsTrigger)
                    {
                        bool gameObjecctExists = hitboxBase.GameObjectsTriggerEnter.Exists(x => x == GameObject);

                        if (!gameObjecctExists)
                        {
                            hitboxBase.GameObjectsTriggerEnter.Add(GameObject);

                            foreach (Script script in scripts)
                            {
                                script.OnTriggerEnter(gameObject);
                            }
                        }
                    }
                    else if (!_hitboxBase.IsTrigger)
                    {
                        Physics.RepellingObjects(GameObject, gameObject);

                        foreach (Script script in scripts)
                        {
                            script.OnCollision(gameObject);
                        }

                        if (gameObject.HasScript<Rightbody>())
                        {
                            var rightbodyB = gameObject.GetScript<Rightbody>();

                            var m1 = Weight;
                            var m2 = rightbodyB.Weight;
                            var v1 = Velocity;
                            var v2 = rightbodyB.Velocity;

                            float e = (Elasticity + rightbodyB.Elasticity) / 2;
                            var totalMass = m1 + m2;
                            var velocityDiff = v2 - v1;

                            var newV1 = v1 + e * (2 * m2 / totalMass) * velocityDiff;
                            var newV2 = v2 + e * (2 * m1 / totalMass) * -velocityDiff;

                            Velocity = newV1;
                            rightbodyB.Velocity = newV2;
                        }
                    }
                }
                else if (!_hitboxBase.IsTrigger && hitboxBase.IsTrigger)
                {
                    bool gameObjecctExists = hitboxBase.GameObjectsTriggerEnter.Exists(x => x == GameObject);

                    if (gameObjecctExists)
                    {
                        hitboxBase.GameObjectsTriggerEnter.Remove(GameObject);

                        foreach (Script script in scripts)
                        {
                            script.OnTriggerLeave(gameObject);
                        }
                    }
                }
            }
        }

        private void ForceMove()
        {
            var move = Physics.DistanceTraveled(Friction, Weight, 9.8f, ref _velocity);
            Position += move;
        }
    }
}
