using UnityEngine;

namespace RayTrace
{
    public class DiffuseAndReflectScene
    {
        public const int SAMPLETIMES = 50;
        public const int MAX_SCATTER_TIME = 10;

     static Color GetColorForTestMetal(Ray ray, HitableList hitableList, int depth)
        {
            HitRecord record = new HitRecord();
            if (hitableList.Hit(ray, 0.0001f, float.MaxValue, ref record))
            {
                Ray r = new Ray(Vector3.zero, Vector3.zero);
                Color attenuation = Color.black;
                if (depth < MAX_SCATTER_TIME && record.material.Scatter(ray, record, ref attenuation, ref r))
                {
                    Color c = GetColorForTestMetal(r, hitableList, depth + 1);
                    return new Color(c.r * attenuation.r, c.g * attenuation.g, c.b * attenuation.b);
                }
                         
                    return Color.black;           
            }
            float t = 0.5f * ray.normalDirection.y + 1f;
            return (1 - t) * new Color(1, 1, 1) + t * new Color(0.5f, 0.7f, 1);
        }

       public static Color[] MakeColors()
        {
            int width=CreatPNG.WIDTH;
            int height = CreatPNG.HEIGHT;
            //视锥体的左下角、长宽和起始扫射点设定
            Vector3 lowLeftCorner = new Vector3(-2, -1, -1);
            Vector3 horizontal = new Vector3(4, 0, 0);
            Vector3 vertical = new Vector3(0, 2, 0);
            Vector3 original = new Vector3(0, 0, 0);
            int l = width * height;
            HitableList hitableList = new HitableList();
            hitableList.AddHitable(new HitableSphere(new Vector3(0, 0, -1), 0.5f, new Lambert(new Color(0.8f, 0.3f, 0.3f))));
            hitableList.AddHitable(new HitableSphere(new Vector3(0, -100.5f, -1), 100f, new Lambert(new Color(0.8f, 0.8f, 0.0f))));
            hitableList.AddHitable(new HitableSphere(new Vector3(1, 0, -1), 0.5f, new Metal(new Color(0.8f, 0.6f, 0.2f))));
            hitableList.AddHitable(new HitableSphere(new Vector3(-1, 0, -1), 0.5f, new Metal(new Color(0.8f, 0.8f, 0.8f))));
            Color[] colors = new Color[l];
           
    
            for (int j = height - 1; j >= 0; j--)
                for (int i = 0; i < width; i++)
                {
                    Color color = new Color(0, 0, 0);
                    for (int s = 0; s < SAMPLETIMES; s++)
                    {
                        Ray r = new Ray(original,
                            lowLeftCorner + horizontal * ((j + Random.Range(0, 1f)) / (float) width) +
                            vertical * ((i + Random.Range(0, 1f)) / (float) height));
                        color += GetColorForTestMetal(r, hitableList, 0);
                    }
                    color /=SAMPLETIMES;
                    //为了使球体看起来更亮，改变gamma值
                    color = new Color(Mathf.Sqrt(color.r), Mathf.Sqrt(color.g), Mathf.Sqrt(color.b), 1f);
                    color.a = 1f;
                    colors[i + j * width] = color;
                }
            return colors;
        }
    }
}