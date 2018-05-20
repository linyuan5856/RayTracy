using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray
{

	private Vector3 origin;
	private Vector3 direction;
	private float t;
	
	public Ray(Vector3 o,Vector3 d)
	{
		this.origin = o;
		this.direction = d;
	}

	public Vector3 GetPoint(float t)
	{
		return this.origin + this.direction * t;
	}
}
