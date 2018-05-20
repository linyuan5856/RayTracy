using UnityEngine;
using Ray = RayTrace.Ray;

public class MultipleSphereScene
{
    public const int SAMPLETIMES = 10;

    private static Color GetColorFromScene(Ray ray, HitableList hitableList)
    {
        HitRecord record = new HitRecord();
        if (hitableList.Hit(ray, 0f, float.MaxValue, ref record))
        {
            return 0.5f * new Color(record.normal.x + 1, record.normal.y + 1, record.normal.z + 1, 1f);
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
        hitableList.AddHitable(new HitableSphere(new Vector3(0, 0, -1), 0.5f));
        hitableList.AddHitable(new HitableSphere(new Vector3(0, -100.5f, -1), 100f));
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
                    newColor += GetColorFromScene(ray, hitableList);
                }

                colors[j + i * WIDTH] =newColor/SAMPLETIMES;
                colors[j + i * WIDTH].a = 1;
            }
        }

        return colors;
    }
}