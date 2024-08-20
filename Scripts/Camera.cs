using System;

#pragma warning disable CS0168

namespace SharpNEX.Engine.Scripts
{
    [Serializable]
    public class Camera : Script
    {
        public int ID;

        public static int СurrentCamera = 1;

        internal static Camera GetCamera(int id)
        {
            var gameObjects = Game.Scene.GetGameObjects();
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
