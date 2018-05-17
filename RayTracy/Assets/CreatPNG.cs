using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework.Internal;
using UnityEditor;
using UnityEngine;

public class CreatPNG : MonoBehaviour
{
    private const int WIDTH = 800;
    private const int HEIGHT = 600;

    // Use this for initialization
    void Start()
    {
        Creat_PNG(WIDTH, HEIGHT);
    }


    public static void Creat_PNG(int width, int height)
    {
        int total = width * height;
        Color[] colors = new Color[total];

        for (int j = height - 1; j >= 0; j--)
        {
            for (int i = 0; i < width; i++)
            {
                colors[i + j * width] = new Color(i / (float) width, j / (float) height, 0.2f);
            }
        }

        Texture2D tex = new Texture2D(width, height, TextureFormat.RGBA32, false);

        tex.SetPixels(colors);
        tex.Apply();

        byte[] bytes = tex.EncodeToJPG();

        string path = "C:/Project/Unity/RayTrace/Assets/Texture/test.jpg";


        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            using (BinaryWriter bw = new BinaryWriter(fs))
            {
                bw.Write(bytes);
            }
        }


        Debug.LogWarning("Creat PNG Done");
    }
}