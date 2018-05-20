using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ray=RayTrace.Ray;

public class SphereNormal  {

	static float HitSphereForTestNormal(Vector3 center, float radius, Ray ray)
	{
		var oc = ray.origin - center;
		float a = Vector3.Dot(ray.direction, ray.direction);
		float b = 2f * Vector3.Dot(oc, ray.direction);
		float c = Vector3.Dot(oc, oc) - radius * radius;
		//实际上是判断这个方程有没有根，如果有2个根就是击中
		float discriminant = b * b - 4 * a * c;
		if (discriminant < 0)
		{
			return -1;
		}
		else
		{
			//返回距离最近的那个根
			return (-b - Mathf.Sqrt(discriminant)) / (2f * a);
		}
	}

	static Color GetColorForTestNormal(Ray ray)
	{
		float t = HitSphereForTestNormal(new Vector3(0, 0, -1), 0.5f, ray);
		if (t > 0)
		{
			Vector3 normal = Vector3.Normalize(ray.GetPoint(t) - new Vector3(0,0,-1));
			return 0.5f * new Color(normal.x + 1, normal.y + 1, normal.z + 1, 2f);
		}
		t = 0.5f * ray.normalDirection.y + 1f;
		return (1 - t) * new Color(1, 1, 1) + t * new Color(0.5f, 0.7f, 1);
	}
	
	public static Color[] MakeColors()
	{
		int WIDTH = CreatPNG.WIDTH;
		int HEIGHT = CreatPNG.HEIGHT;

		int total = WIDTH * HEIGHT;
		Color[] colors = new Color[total];
		Vector3 origin=Vector3.zero;
		Vector3 horizontal=new Vector3(4,0,0);
		Vector3 vertical=new Vector3(0,2,0);
		Vector3 leftDown=new Vector3(-2,-1,-1);
        

		for (int j = HEIGHT - 1; j >= 0; j--)
		{
			for (int i = 0; i < WIDTH; i++)
			{
				colors[i + j * WIDTH] = GetColorForTestNormal(new Ray(origin,
					leftDown + (i / (float) WIDTH)*horizontal+(j / (float) HEIGHT)*vertical));
			}
		}

		return colors;
	}
}
