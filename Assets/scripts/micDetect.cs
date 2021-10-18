using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class micDetect : MonoBehaviour
{
    public RawImage image;
    Texture2D restingTex;
    Texture2D talkingTex;
    public Animator imageAnimator;
    public Dropdown dropList;

    float timeRemaining = 0.7f;

    public AudioClip microphoneInput;
    public float sensitivity;

    // Start is called before the first frame update
    void Start()
    {
        if (Microphone.devices.Length > 0)
        {
            microphoneInput = Microphone.Start(Microphone.devices[dropList.value], true, 999, 44100);
        }

        restingTex = new Texture2D(2, 2);
        talkingTex = new Texture2D(2, 2);
        restingTex.LoadImage(File.ReadAllBytes("./resting.png"));
        talkingTex.LoadImage(File.ReadAllBytes("./talking.png"));

        image.texture = restingTex;
    }

    // Update is called once per frame
    void Update()
    {
        float level = getMicLevel();

        timeRemaining -= Time.deltaTime;

        if (level > sensitivity && timeRemaining <= 0)
        {
            imageAnimator.Play("bounce");
            image.texture = talkingTex;
            timeRemaining = 0.7f;
        }
        else if(level > sensitivity)
        {
            timeRemaining = 0.7f;
        }
        else if (timeRemaining <= 0)
        {
            image.texture = restingTex;
            imageAnimator.Play("bounceDown");
        }


    }

    float getMicLevel()
    {
        int dec = 128;
        float[] waveData = new float[dec];
        int micPosition = Microphone.GetPosition(Microphone.devices[dropList.value]) - (dec + 1);
        microphoneInput.GetData(waveData, micPosition);

        float levelMax = 0;
        for (int i = 0; i < dec; i++)
        {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak)
            {
                levelMax = wavePeak;
            }
        }
        float level = Mathf.Sqrt(Mathf.Sqrt(levelMax));

        return level;
    }
}

