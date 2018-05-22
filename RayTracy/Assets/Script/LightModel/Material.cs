using UnityEngine;

namespace RayTrace
{
    public  abstract class Material
    {

        public abstract bool Scatter(Ray rayIn,HitRecord hitRecord,ref Vector3 albedo,ref Ray scatterRay);
    }
}

