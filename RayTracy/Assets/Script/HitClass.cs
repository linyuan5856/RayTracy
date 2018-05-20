using System.Collections.Generic;
using UnityEngine;
using Ray = RayTrace.Ray;

public class HitClass
{
}

public class HitRecord
{
    public float t;
    public Vector3 p;
    public Vector3 normal;
}

public class Sphere2 : Hitable
{
    public Vector3 center;
    public float radius;

    public Sphere2(Vector3 cen, float rad)
    {
        center = cen;
        radius = rad;
    }

    public override bool Hit(Ray ray, float t_min, float t_max, ref HitRecord rec)
    {
        var oc = ray.origin - center;
        float a = Vector3.Dot(ray.direction, ray.direction);
        float b = 2f * Vector3.Dot(oc, ray.direction);
        float c = Vector3.Dot(oc, oc) - radius * radius;

        float discriminant = b * b - 4 * a * c;
        if (discriminant > 0)
        {
            float temp = (-b - Mathf.Sqrt(discriminant)) / a * 0.5f;
            if (temp < t_max && temp > t_min)
            {
                rec.t = temp;
                rec.p = ray.GetPoint(rec.t);
                rec.normal = (rec.p - center).normalized;
                return true;
            }

            temp = (-b + Mathf.Sqrt(discriminant)) / a * 0.5f;
            if (temp < t_max && temp > t_min)
            {
                rec.t = temp;
                rec.p = ray.GetPoint(rec.t);
                rec.normal = (rec.p - center).normalized;
                return true;
            }
        }

        return false;
    }
}


public abstract class Hitable
{
    public abstract bool Hit(Ray ray, float t_min, float t_max, ref HitRecord hit);
}

public class HitableList : Hitable
{
    private List<Hitable> hitLists;

    public HitableList()
    {
        hitLists = new List<Hitable>();
    }

    public void AddHitable(Hitable hitable)
    {
        if (!this.hitLists.Contains(hitable))
            this.hitLists.Add(hitable);
    }

    public override bool Hit(Ray ray, float t_min, float t_max, ref HitRecord hit)
    {
        HitRecord tempRecord = new HitRecord();
        bool hitted = false;
        float closest = t_max;
        for (int i = 0; i < this.hitLists.Count; i++)
        {
            if (this.hitLists[i].Hit(ray, t_min, closest, ref tempRecord ))
            {
                 hit=tempRecord;
                closest = tempRecord.t;
                hitted = true;
            }
        }

        return hitted;
    }
}