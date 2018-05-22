using UnityEngine;

namespace RayTrace
{
    public class Lambert:Material
    {
        private Vector3 mAlbedo;
        public Lambert(Vector3 albedo)
        {
            this.mAlbedo = albedo;
        }

        public override bool Scatter(Ray rayIn, HitRecord hitRecord, ref Vector3 albedo, ref Ray scatterRay)
        {
            Vector3 target = hitRecord.p + hitRecord.normal + GetRandomUnitInSphere();
            scatterRay=new Ray(hitRecord.p,target-hitRecord.p);
            albedo = this.mAlbedo;
            return true;
        }

        private Vector3 GetRandomUnitInSphere()
        {
            Vector3 p = 2f * new Vector3(Random.Range(0,1f),Random.Range(0,1),Random.Range(0,1f))-Vector3.one;
            p = p.normalized * Random.Range(0, 1f);

            return p;
        }
    }
}