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
    bool blinkAvailable = false;
    bool blinking = false;
    public UI uiReference;
    List<Texture2D> emotions = new List<Texture2D>();
    Texture2D currentTex;

    float timeRemaining = 1.0f;
    float blinkCount = 5.0f;

    public AudioClip microphoneInput;

    // Start is called before the first frame update
    void Start()
    {
        int emoteCounter = 0;

        if (Microphone.devices.Length > 0)
        {
            microphoneInput = Microphone.Start(Microphone.devices[dropList.value], true, 999, 44100);
        }

        foreach (string file in Directory.GetFiles("./images/"))
        {
            if (file.Contains("emote"))
            {
                emotions.Add(new Texture2D(2, 2, TextureFormat.ARGB32, false));
                emotions[emoteCounter].LoadImage(File.ReadAllBytes(file));
                emoteCounter++;
            }
            else if (file.Contains("resting"))
            {
                restingTex = new Texture2D(2, 2, TextureFormat.ARGB32, false);
                restingTex.LoadImage(File.ReadAllBytes(file));
            }
            else if (file.Contains("talking"))
            {
                talkingTex = new Texture2D(2, 2, TextureFormat.ARGB32, false);
                talkingTex.LoadImage(File.ReadAllBytes(file));
            }
            else if (file.Contains("blinking"))
            {
                blinkingTex = new Texture2D(2, 2, TextureFormat.ARGB32, false);
                blinkingTex.LoadImage(File.ReadAllBytes(file));
                blinkAvailable = true;
            }
        }

        image.texture = restingTex;
        currentTex = restingTex;
    }

    // Update is called once per frame
    void Update()
    {
        float level = getMicLevel();

        timeRemaining -= Time.deltaTime;
        blinkCount -= Time.deltaTime;

        if (level > uiReference.sensSlider.value && timeRemaining <= 0 && !blinking)
        {
            if (uiReference.bounceToggle.isOn)
            {
                imageAnimator.Play("bounce");
            }
            image.texture = talkingTex;
            timeRemaining = uiReference.revertSlider.value;
        }
        else if (level > uiReference.sensSlider.value && !blinking)
        {
            timeRemaining = uiReference.revertSlider.value;
        }
        else if (timeRemaining <= 0)
        {
            image.texture = currentTex;
            if (uiReference.bounceToggle.isOn)
            {
                imageAnimator.Play("bounceDown");
            }
        }

        if (blinkAvailable)
        {
            if (blinkCount <= 0 && timeRemaining <= 0)
            {
                StartCoroutine("PlayBlink");
            }
        }

        if (Input.anyKey)
        {
            SetEmote(Input.inputString);
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
        blinking = true;
        image.texture = blinkingTex;
        yield return new WaitForSeconds(0.1f);
        image.texture = restingTex;
        blinkCount = Random.Range(6f, 10f);
        blinking = false;
    }
    void SetEmote(string inputString)
    {
        if (inputString == "0") { currentTex = restingTex; }
        if (inputString == uiReference.emoteBindings[0] && emotions.Count >= 1) { currentTex = emotions[0]; }
        if (inputString == uiReference.emoteBindings[1] && emotions.Count >= 2) { currentTex = emotions[1]; }
        if (inputString == uiReference.emoteBindings[2] && emotions.Count >= 3) { currentTex = emotions[2]; }
        if (inputString == uiReference.emoteBindings[3] && emotions.Count >= 4) { currentTex = emotions[3]; }
        if (inputString == uiReference.emoteBindings[4] && emotions.Count >= 5) { currentTex = emotions[4]; }
    }
}