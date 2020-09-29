using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Threading;
using System;
using UnityToolbag;
using TMPro;

public class OpencvManager : MonoBehaviour
{
    [DllImport("unity_opencv_plugin", EntryPoint = "init")]
    public static extern int init();


    [DllImport("unity_opencv_plugin", EntryPoint = "process")]
    public static extern int process(Color32[] raw, int width, int height);

    public RenderTexture cameraTexture;
    public TextMeshProUGUI toggleMonitoringButtonText;
    Texture2D tex;
    Rect sourceRect;


    Thread thread;

    List<Color32[]> snapshots = new List<Color32[]>();

    int width;
    int height;

    bool isMonitoring;

    private void Awake()
    {
        
    }

    void Start()
    {
        tex = new Texture2D(cameraTexture.width, cameraTexture.height);
        sourceRect = new Rect(0, 0, tex.width, tex.height);
        width = tex.width;
        height = tex.height;
        init();
        thread = new Thread(new ThreadStart(processInOpenCv));
        thread.Start();
        StartCoroutine(Execute());
    }

    public void ToggleMonitoring() {
        isMonitoring = !isMonitoring;
        toggleMonitoringButtonText.text = isMonitoring ? "Stop Monitoring" : "Start Monitoring";
    }


    IEnumerator Execute()
    {
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            yield return new WaitForEndOfFrame();
            captureScreen();
            yield return new WaitForSeconds(0.5f);            
        }
    }

    void processInOpenCv()
    {
        while (true)
        {
            //Thread.Sleep(100);
            //Debug.Log(snapshots.Count);
            if (snapshots.Count > 0)
            {                
                Color32[] rawImg = snapshots[0];
               
                if (rawImg != null)
                {
                    for (int i = 0; i < rawImg.Length; i++)
                    {
                        byte r = rawImg[i].r;
                        byte g = rawImg[i].g;
                        byte b = rawImg[i].b;

                        rawImg[i].r = b;
                        rawImg[i].b = r;
                        rawImg[i].a = 255;
                    }

                    Array.Reverse(rawImg);

                    Dispatcher.Invoke(() =>
                    {
                        int result = process(rawImg, width, height);
                        Debug.Log(result);
                    });

                    snapshots.RemoveAt(0);

                }

            }
        }
    }



    public void captureScreen()
    {
        if (!isMonitoring) return;

        RenderTexture.active = cameraTexture;

        tex.ReadPixels(sourceRect, 0, 0);

        Color32[] rawImg = tex.GetPixels32();

        if (rawImg != null)
            snapshots.Add(rawImg);
        /*System.Array.Reverse(rawImg);
        process(rawImg, width, height);*/
    }
}
