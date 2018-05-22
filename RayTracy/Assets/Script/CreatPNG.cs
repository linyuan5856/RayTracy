using System.IO;
using RayTrace;
using UnityEditor;
using UnityEngine;

public class CreatPNG : MonoBehaviour
{
    public const int WIDTH = 800;
    public const int HEIGHT = 600;
    public const string PATH = "Assets/Texture/DiffuseAndMetal.jpg";


    private static Color[] MakeTexture()
    {
        int total = WIDTH * HEIGHT;
        Color[] colors = new Color[total];

        for (int j = HEIGHT - 1; j >= 0; j--)
        {
            for (int i = 0; i < WIDTH; i++)
            {
                colors[i + j * WIDTH] = new Color(i / (float) WIDTH, j / (float) HEIGHT, 0.2f);
            }
        }

        return colors;
    }

    public static void Creat_FileInternal(Color[] colors)
    {
        Texture2D tex = new Texture2D(WIDTH, HEIGHT, TextureFormat.RGBA32, false);

        tex.SetPixels(colors);
        tex.Apply();

        byte[] bytes = tex.EncodeToJPG();

        using (FileStream fs = new FileStream(PATH, FileMode.Create))
        {
            using (BinaryWriter bw = new BinaryWriter(fs))
            {
                bw.Write(bytes);
            }
        }


        Debug.LogWarning("Creat PNG Done");
        AssetDatabase.Refresh();
    }

    [MenuItem("RayTrace/CreatTexture")]
    public static void CreatFile()
    {
        //Color[] colors = MakeTexture();
        //Color[] colors = Sphere.MakeColors();
        //Color[] colors = SphereNormal.MakeColors();
        //Color[] colors = MultipleSphereScene.MakeColors();
        //Color[] colors = Diffuse.MakeColors();
        Color[] colors = DiffuseAndReflectScene.MakeColors();
        Creat_FileInternal(colors);
    }
}