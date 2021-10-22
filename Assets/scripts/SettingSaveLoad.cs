using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class settings
{
    public int micDevice;
    public float sensitivity;
    public float background;
    public float revertDelay;
    public bool bounce;
    public List<string> emotes;
}

public class SettingSaveLoad : MonoBehaviour
{
    public UI uiReference;
    settings saveSettings = new settings();

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            StreamReader settingsFile = new StreamReader("./settings.json");
            saveSettings = JsonUtility.FromJson<settings>(settingsFile.ReadLine());

            uiReference.micList.value = saveSettings.micDevice;
            uiReference.MicValueChanged(uiReference.micList.value);
            uiReference.sensSlider.value = saveSettings.sensitivity;
            uiReference.hueSlider.value = saveSettings.background;
            uiReference.HueSliderValueChanged(uiReference.hueSlider.value);
            uiReference.revertSlider.value = saveSettings.revertDelay;
            uiReference.bounceToggle.isOn = saveSettings.bounce;
            uiReference.emoteBindings = saveSettings.emotes;

            uiReference.emote1Button.GetComponentInChildren<Text>().text = "Emote 1: " + uiReference.emoteBindings[0];
            uiReference.emote2Button.GetComponentInChildren<Text>().text = "Emote 2: " + uiReference.emoteBindings[1];
            uiReference.emote3Button.GetComponentInChildren<Text>().text = "Emote 3: " + uiReference.emoteBindings[2];
            uiReference.emote4Button.GetComponentInChildren<Text>().text = "Emote 4: " + uiReference.emoteBindings[3];
            uiReference.emote5Button.GetComponentInChildren<Text>().text = "Emote 5: " + uiReference.emoteBindings[4];
            uiReference.emoteResetButton.GetComponentInChildren<Text>().text = "Reset: " + uiReference.emoteBindings[5];
        }
        catch (FileNotFoundException)
        {

        }
    }

    private void OnDestroy()
    {
        StreamWriter writer = new StreamWriter("./settings.json");

        saveSettings.micDevice = uiReference.micList.value;
        saveSettings.sensitivity = uiReference.sensSlider.value;
        saveSettings.background = uiReference.hueSlider.value;
        saveSettings.revertDelay = uiReference.revertSlider.value;
        saveSettings.bounce = uiReference.bounceToggle.isOn;
        saveSettings.emotes = uiReference.emoteBindings;

        string settingsJson = JsonUtility.ToJson(saveSettings);

        writer.WriteLine(settingsJson);
        writer.Close();
    }
}
