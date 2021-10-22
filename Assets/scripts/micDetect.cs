using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityRawInput;

public class MicDetect : MonoBehaviour
{
    public RawImage image;
    Texture2D restingTex;
    Texture2D talkingTex;
    Texture2D blinkingTex;
    Texture2D currentTex;
    float revertDelay = 1.0f;

    bool blinkAvailable = false;
    bool blinking = false;
    float blinkCount = 5.0f;

    public Animator imageAnimator;
    public Dropdown micList;

    public UI uiReference;
    List<Texture2D> emotions = new List<Texture2D>();
    public AudioClip microphoneInput;

    // Start is called before the first frame update
    void Start()
    {
        int emoteCounter = 0;

        RawKeyInput.Start(true);
        RawKeyInput.OnKeyDown += SetEmote;

        if (Microphone.devices.Length > 0)
        {
            microphoneInput = Microphone.Start(Microphone.devices[micList.value], true, 999, 44100);
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

        revertDelay -= Time.deltaTime;
        blinkCount -= Time.deltaTime;

        if (level > uiReference.sensSlider.value && revertDelay <= 0 && !blinking)
        {
            if (uiReference.bounceToggle.isOn)
            {
                imageAnimator.Play("bounce");
            }
            image.texture = talkingTex;
            revertDelay = uiReference.revertSlider.value;
        }
        else if (level > uiReference.sensSlider.value && !blinking)
        {
            revertDelay = uiReference.revertSlider.value;
        }
        else if (revertDelay <= 0 && image.texture == talkingTex)
        {
            image.texture = currentTex;
            if (uiReference.bounceToggle.isOn)
            {
                imageAnimator.Play("bounceDown");
            }
        }

        if (blinkAvailable)
        {
            if (blinkCount <= 0 && revertDelay <= 0)
            {
                StartCoroutine("PlayBlink");
            }
        }
    }

    private void OnDestroy()
    {
        RawKeyInput.OnKeyDown -= SetEmote;
        RawKeyInput.Stop();
    }
    float getMicLevel()
    {
        int dec = 128;
        float[] waveData = new float[dec];
        int micPosition = Microphone.GetPosition(Microphone.devices[micList.value]) - (dec + 1);
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
        image.texture = currentTex;
        blinkCount = Random.Range(6f, 10f);
        blinking = false;
    }

    void SetEmote(RawKey inputString)
    {
        if (!uiReference.settingKey)
        {
            if (inputString.ToString() == uiReference.emoteBindings[0] && emotions.Count >= 1) { currentTex = emotions[0]; image.texture = currentTex; }
            if (inputString.ToString() == uiReference.emoteBindings[1] && emotions.Count >= 2) { currentTex = emotions[1]; image.texture = currentTex; }
            if (inputString.ToString() == uiReference.emoteBindings[2] && emotions.Count >= 3) { currentTex = emotions[2]; image.texture = currentTex; }
            if (inputString.ToString() == uiReference.emoteBindings[3] && emotions.Count >= 4) { currentTex = emotions[3]; image.texture = currentTex; }
            if (inputString.ToString() == uiReference.emoteBindings[4] && emotions.Count >= 5) { currentTex = emotions[4]; image.texture = currentTex; }
            if (inputString.ToString() == uiReference.emoteBindings[5]) { currentTex = restingTex; image.texture = currentTex; }
        }
        else
        {
            uiReference.bindValue = inputString.ToString();
        }
    }
}