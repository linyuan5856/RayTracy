using UnityEngine;

namespace RayTrace
{
    public class Camera
    {
        public Vector3 position;
        public Vector3 lowLeftCorner;
        public Vector3 horizontal;
        public Vector3 vertical;
        public Vector3 u, v, w;
        public float radius;
        
        public Camera(Vector3 lookFrom, Vector3 lookat, Vector3 vup, float vfov, float aspect, float r = 0, float focus_dist = 1)
        {
            radius = r * 0.5f;
            float unitAngle = Mathf.PI / 180f * vfov;
            float halfHeight = Mathf.Tan(unitAngle * 0.5f);
            float halfWidth = aspect * halfHeight;
            position = lookFrom;
            w = (lookat - lookFrom).normalized;
            u = Vector3.Cross(vup, w).normalized;
            v = Vector3.Cross(w, u).normalized;
            lowLeftCorner = lookFrom + w * focus_dist - halfWidth * u * focus_dist - halfHeight * v * focus_dist;
            horizontal = 2 * halfWidth * focus_dist * u;
            vertical = 2 * halfHeight * focus_dist * v;
        }
        public Ray CreateRay(float x, float y)
        {
            
            if (radius == 0f)
                return new Ray(position, lowLeftCorner + x * horizontal + y * vertical - position);
            else
            {
                Vector3 rd = radius * GetRandomUnitInSphere();
                Vector3 offset = rd.x * u + rd.y * v;
                return new Ray(position + offset, lowLeftCorner + x * horizontal + y * vertical - position - offset);
            }
        }
        
        private Vector3 GetRandomUnitInSphere()
        {
            Vector3 p = 2f * new Vector3(Random.Range(0,1f),Random.Range(0,1),Random.Range(0,1f))-Vector3.one;
            p = p.normalized * Random.Range(0, 1f);

            return p;
        }
    }
}