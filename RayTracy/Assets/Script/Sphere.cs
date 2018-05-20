using UnityEngine;
using Ray = RayTrace.Ray;

public class Sphere
{

    static bool IsRayCastSphere(Ray ray,Vector3 sphereOrigin,float radiu)
    {
        Vector3 oc = ray.direction - sphereOrigin;
        float a = Vector3.Dot(ray.direction, ray.direction);
        float b = 2f*Vector3.Dot(ray.direction,oc);
        float c = Vector3.Dot(oc,oc)- radiu * radiu;

        float delta = b * b - 4 * a * c;
        
        return delta>0;
    }

    static Color GetSkyColorFronRay(Ray ray)
    {
        if (IsRayCastSphere(ray,new Vector3(0f,0f,-1f),.5f))
        {
            return Color.red;
        }
        
        float y = (ray.direction.y + 1) /2;
        return (1-y)*new Color(1,1,1)+new Color(0.5f,0.7f,1.0f);
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
                colors[i + j * WIDTH] = GetSkyColorFronRay(new Ray(origin,
                    leftDown + (i / (float) WIDTH)*horizontal+(j / (float) HEIGHT)*vertical));
            }
        }

        return colors;
    }
}