using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ray = RayTrace.Ray;

public class Diffuse  {

	public const int SAMPLETIMES = 50;

	private static Vector3 GetRandomDir()
	{
		Vector3 p=2f*new Vector3(Random.Range(0,1f),Random.Range(0,1f),Random.Range(0,1f))-Vector3.one;
		p=p.normalized * Random.Range(0, 1f);
		return p;
	}

	private static Color GetColorForTestDiffusing(Ray ray, HitableList hitableList)
	{
		HitRecord record = new HitRecord();
		if (hitableList.Hit(ray, 0.0001f, float.MaxValue, ref record))
		{
			Vector3 target = record.p + record.normal + GetRandomDir();
			return 0.5f * GetColorForTestDiffusing(new Ray(record.p, target - record.p), hitableList);
		}
		
		float t = (ray.direction.y + 1) * .5f;

		return (1 - t) * Color.white + new Color(0.5f, 0.7f, 1.0f);
	}


	public static Color[] MakeColors()
	{
		int WIDTH = CreatPNG.WIDTH;
		int HEIGHT = CreatPNG.HEIGHT;
		int total = WIDTH * HEIGHT;
		Color[] colors = new Color[total];


		Vector3 origin = Vector3.zero;
		Vector3 horizontal = new Vector3(4, 0, 0);
		Vector3 vertical = new Vector3(0, 2, 0);
		Vector3 leftDownCorner = new Vector3(-2, -1, -1);

		HitableList hitableList = new HitableList();
		hitableList.AddHitable(new HitableSphere(new Vector3(0, 0, -1), 0.5f,null));
		hitableList.AddHitable(new HitableSphere(new Vector3(0, -100.5f, -1), 100f,null));
		for (int i = HEIGHT - 1; i >= 0; i--)
		{
			for (int j = 0; j < WIDTH; j++)
			{
				Ray ray = new Ray(origin,
					leftDownCorner + horizontal * ((j + Random.Range(0, 1)) / (float) WIDTH) +
					vertical * ((i + Random.Range(0, 1)) / (float) HEIGHT));
				Color newColor=Color.black;
				for (int k = 0; k < SAMPLETIMES; k++)
				{
					newColor+= GetColorForTestDiffusing(ray, hitableList);
				}
				colors[j + i * WIDTH] =newColor/SAMPLETIMES;
				colors[j + i * WIDTH].a = 1;
				Color temp=colors[j + i * WIDTH];
				colors[j + i * WIDTH]= new Color(Mathf.Pow(temp.r, 1 / 2.2f),Mathf.Pow(temp.g, 1 / 2.2f),Mathf.Pow(temp.b, 1 / 2.2f));
				
			}
		}

		return colors;
	}
	
	
	
	
	
}
