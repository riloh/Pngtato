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
    Texture2D blinkingTex;
    public Animator imageAnimator;
    public Dropdown dropList;
    bool blinking;
    public UI uiReference;

    float timeRemaining = 1.0f;
    float blinkCount = 5.0f;

    public AudioClip microphoneInput;

    // Start is called before the first frame update
    void Start()
    {
        if (Microphone.devices.Length > 0)
        {
            microphoneInput = Microphone.Start(Microphone.devices[dropList.value], true, 999, 44100);
        }

        restingTex = new Texture2D(2, 2);
        talkingTex = new Texture2D(2, 2);
        blinkingTex = new Texture2D(2, 2);
        restingTex.LoadImage(File.ReadAllBytes("./images/resting.png"));
        talkingTex.LoadImage(File.ReadAllBytes("./images/talking.png"));
        
        try {
            blinkingTex.LoadImage(File.ReadAllBytes("./images/blinking.png"));
            blinking = true;
        }
        catch (FileNotFoundException)
        {
            blinking = false;
        }
        Debug.Log(blinking);

        image.texture = restingTex;
    }

    // Update is called once per frame
    void Update()
    {
        float level = getMicLevel();

        timeRemaining -= Time.deltaTime;
        blinkCount -= Time.deltaTime;

        if (level > uiReference.sensSlider.value && timeRemaining <= 0)
        {
            if (uiReference.bounceToggle.isOn)
            {
                imageAnimator.Play("bounce");
            }
            image.texture = talkingTex;
            timeRemaining = uiReference.revertSlider.value;
        }
        else if(level > uiReference.sensSlider.value)
        {
            timeRemaining = uiReference.revertSlider.value;
        }
        else if (timeRemaining <= 0)
        {
            image.texture = restingTex;
            if (uiReference.bounceToggle.isOn)
            {
                imageAnimator.Play("bounceDown");
            }
        }

        if (blinking)
        {
            if(blinkCount <= 0 && timeRemaining <= 0)
            {
                StartCoroutine("PlayBlink");
            }
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

    IEnumerator PlayBlink()
    {
        image.texture = blinkingTex;
        yield return new WaitForSeconds(0.15f);
        image.texture = restingTex;
        blinkCount = Random.Range(3f, 10f);
    }
}


