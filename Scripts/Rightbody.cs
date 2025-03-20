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

            var gameObjects = Game.Scene.GetGameObjects();
            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject == GameObject)
                {
                    continue;
                }

                var hitboxBase = gameObject.GetScriptFromBaseType<HitboxBase>();
                if (hitboxBase == null)
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
                            var righitbody = gameObject.GetScript<Rightbody>();

                            var newVelocity = Velocity * Weight / (Weight + righitbody.Weight);

                            righitbody.Velocity = newVelocity;
                            Velocity = newVelocity;
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
