using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Material = RayTrace.Material;

public class HitableSphere:Hitable {

	public Vector3 center;
	public float radius;
	public Material material;

	public HitableSphere(Vector3 cen, float rad,Material m)
	{
		center = cen;
		radius = rad;
		this.material= m;
	}

	public override bool Hit(RayTrace.Ray ray, float t_min, float t_max, ref HitRecord rec)
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
				rec.material = material;
				return true;
			}

			temp = (-b + Mathf.Sqrt(discriminant)) / a * 0.5f;
			if (temp < t_max && temp > t_min)
			{
				rec.t = temp;
				rec.p = ray.GetPoint(rec.t);
				rec.normal = (rec.p - center).normalized;
				rec.material = material;
				return true;
			}
		}

		return false;
	}
}
