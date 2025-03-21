﻿using System.Linq;
using System.Collections.Generic;

using SharpNEX.Engine.Scripts;

namespace SharpNEX.Engine.Utils
{
    internal static class Physics
    {
        private static bool PointInRectangle(Vector polygonPosition, List<Vector> polygon, Vector point)
        {
            float minX = polygonPosition.X + polygon.Min(x => x.X);
            float minY = polygonPosition.Y + polygon.Min(x => x.Y);
            float maxX = polygonPosition.X + polygon.Max(x => x.X);
            float maxY = polygonPosition.Y + polygon.Max(x => x.Y);

            if (point.X > minX && point.X < maxX && point.Y > minY && point.Y < maxY)
            {
                return true;
            }

            return false;
        }

        private static bool PointInPolygon(Vector polygonPosition, List<Vector> polygon, Vector point)
        {
            int n = polygon.Count;
            bool isInside = false;

            for (int i = 0, j = n - 1; i < n; j = i++)
            {
                float polygonIX = polygon[i].X + polygonPosition.X;
                float polygonIY = polygon[i].Y + polygonPosition.Y;
                float polygonJX = polygon[j].X + polygonPosition.X;
                float polygonJY = polygon[j].Y + polygonPosition.Y;

                if ((polygonIY > point.Y) != (polygonJY > point.Y) &&
                    (point.X < (polygonJX - polygonIX) * (point.Y - polygonIY) / (polygonJY - polygonIY) + polygonIX))
                {
                    isInside = !isInside;
                }
            }

            return isInside;
        }

        // Проверка нахождения точек из hitbox2 в области hitbox1
        public static bool AreasHitboxesIntersect(HitboxBase hitbox1, HitboxBase hitbox2)
        {
            bool pointInRectangle = false;
            for (int i = 0; i < hitbox1.Points.Count; i++)
            {
                pointInRectangle = PointInRectangle(hitbox2.Position, hitbox2.Points, hitbox1.Points[i] + hitbox1.Position);

                if (pointInRectangle)
                {
                    break;
                }
            }

            return pointInRectangle;
        }

        // Проверка нахождения точек из hitbox2 в полигоне hitbox1
        public static bool HitboxesIntersect(HitboxBase hitbox1, HitboxBase hitbox2)
        {
            bool pointInPolygon = false;
            for (int i = 0; i < hitbox1.Points.Count; i++)
            {
                pointInPolygon = PointInPolygon(hitbox2.Position, hitbox2.Points, hitbox1.Points[i] + hitbox1.Position);

                if (pointInPolygon)
                {
                    break;
                }
            }

            return pointInPolygon;
        }

        // Проверка на столкновение двух хитобоксов
        public static bool ColisionHitboxes(HitboxBase hitbox1, HitboxBase hitbox2)
        {
            bool pointInRectangle = AreasHitboxesIntersect(hitbox1, hitbox2);
            if (!pointInRectangle)
            {
                return false;
            }

            bool pointInPolygon = HitboxesIntersect(hitbox1, hitbox2);

            return pointInPolygon;
        }

        // Нормализованный верктор разницы положения двух объектов
        private static Vector CollisionNormal(Vector positionA, Vector positionB)
        {
            var collisionVector = positionA - positionB;
            var normal = collisionVector.Normalize();

            return normal;
        }

        // Отталкивание объектов при сталкновении
        public static void RepellingObjects(GameObject gameObject, GameObject gameObject1)
        {
            var normal = CollisionNormal(gameObject.Position, gameObject1.Position);
            float pushStrength = 3f;

            var pushVectorA = normal * pushStrength;
            var pushVectorB = -normal * pushStrength;

            gameObject.Position += pushVectorA;
            gameObject1.Position += pushVectorB;
        }

        // Движение объекта
        public static Vector DistanceTraveled(float friction, float weight, float g, ref Vector velocity)
        {
            float force = friction * weight * g; // Сила трения

            // Ускорение
            float accelerationX = force * velocity.X * 0.000001f / weight;
            float accelerationY = force * velocity.Y * 0.000001f / weight;

            // Уменьшение скорости
            velocity.X -= accelerationX * Game.DeltaTime;
            velocity.Y -= accelerationY * Game.DeltaTime;

            // Вектор перемещения
            var result = new Vector(velocity.X * Game.DeltaTime, velocity.Y * Game.DeltaTime);

            return result;
        }
    }
}