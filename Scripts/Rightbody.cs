﻿using System;
using System.Collections.Generic;

using SharpNEX.Engine.Utils;

#pragma warning disable CS0168

namespace SharpNEX.Engine.Scripts
{
    public class Rightbody : Script
    {
        private HitboxBase _hitboxBase;

        public float Friction = 10000;
        public float Weight = 200;

        public Vector Velocity;

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

                    bool colision = Collision.ColisionHitboxes(_hitboxBase, hitboxBase);
                    //TODO: отталкивание
                }
                catch (InvalidOperationException invalidOperationException) { }
            }
        }

        private void ForceMove()
        {
            Vector move = Physics.DistanceTraveled(Friction, Weight, 9.8f, ref Velocity);
            Position += move;
        }
    }
}
