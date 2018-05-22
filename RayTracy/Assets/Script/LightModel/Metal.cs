using UnityEngine;

namespace RayTrace
{
    public class Metal:Material
    {
        Color mAlbedo;
        public Metal(Color a) { mAlbedo = a; }
        
        public override bool Scatter(Ray rayIn, HitRecord hitRecord, ref Color albedo, ref Ray scatterRay)
        {
            Vector3 reflected = Reflect(rayIn.normalDirection, hitRecord.normal);
            scatterRay= new Ray(hitRecord.p, reflected);
            albedo = this.mAlbedo;
            return Vector3.Dot(scatterRay.direction, hitRecord.normal) > 0;
        }


        private Vector3 Reflect(Vector3 viewIn, Vector3 normal)
        {
            return viewIn-2*Vector3.Dot(viewIn,normal)*normal;
        }
    }
}