using System;
using System.Collections.Generic;

#pragma warning disable CS0168

namespace SharpNEX.Engine.Scripts
{
    public class Camera : Script
    {
        public int ID;

        public static int СurrentCamera = 1;

        internal static Camera GetCamera(int id)
        {
            List<GameObject> gameObjects = Game.Scene.GameObjects;

            foreach (var gameObject in gameObjects)
            {
                try
                {
                    var camera = gameObject.GetScript<Camera>();

                    if (camera.ID == id)
                    {
                        return camera;
                    }
                }
                catch (InvalidOperationException invalidOperationException) { }
            }

            throw new InvalidOperationException($"Камера с id \"{id}\" не надена");
        }
    }
}
