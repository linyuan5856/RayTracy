using UnityEngine;

namespace RayTrace
{
    public class Ray
    {
        public Vector3 origin;
        public Vector3 direction;
        public Vector3 normalDirection;
      

        public Ray(Vector3 o, Vector3 d)
        {
            this.origin = o;
            this.direction = d;
            this.normalDirection = d.normalized;
        }

        public Vector3 GetPoint(float t)
        {
            return this.origin + this.direction * t;
        }
    }
}