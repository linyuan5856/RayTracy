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