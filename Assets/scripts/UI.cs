using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI : MonoBehaviour
{
    public Dropdown micList;
    public Slider sensSlider;
    public Text sensLabel;
    public Text sensValue;
    public Slider hueSlider;
    public Text hueLabel;
    public Slider revertSlider;
    public Text revertLabel;
    public Text revertValue;
    public Toggle bounceToggle;
    public Button keybindMenu;
    public Camera mainCam;
    public Button emote1Button, emote2Button, emote3Button, emote4Button, emote5Button, emoteResetButton;
    public micDetect micReference;
    public List<string> emoteBindings = new List<string>();

    float H, S, V;

    // Start is called before the first frame update
    void Start()
    {
        //getting HSV values of camera's BG color
        Color.RGBToHSV(mainCam.backgroundColor, out H, out S, out V);

        sensValue.text = sensSlider.value.ToString("F2");
        revertValue.text = revertSlider.value.ToString("F2");


        //adding listeners for when UI values are changed
        micList.onValueChanged.AddListener(delegate {
            MicValueChanged(micList);
        });
        sensSlider.onValueChanged.AddListener(delegate
        {
            SensSliderChanged(sensSlider);
        });
        hueSlider.onValueChanged.AddListener(delegate
        {
            HueSliderValueChanged(hueSlider);
        });
        revertSlider.onValueChanged.AddListener(delegate
        {
            RevertSliderValueChanged(revertSlider);
        });
        keybindMenu.onClick.AddListener(delegate
        {
            KeybindMenuClicked();
        });
        emote1Button.onClick.AddListener(delegate
        {
            StartCoroutine("Emote1ButtonClicked");
        });
        emote2Button.onClick.AddListener(delegate
        {
            StartCoroutine("Emote2ButtonClicked");
        });
        emote3Button.onClick.AddListener(delegate
        {
            StartCoroutine("Emote3ButtonClicked");
        });
        emote4Button.onClick.AddListener(delegate
        {
            StartCoroutine("Emote4ButtonClicked");
        });
        emote5Button.onClick.AddListener(delegate
        {
            StartCoroutine("Emote5ButtonClicked");
        });
        emoteResetButton.onClick.AddListener(delegate
        {
            StartCoroutine("EmoteResetButtonClicked");
        });

        //Populating mic dropdown with system's audio devices
        if (Microphone.devices.Length > 0)
        {
            List<string> options = new List<string>();
            foreach (var device in Microphone.devices)
            {
                options.Add(device);
            }
            micList.ClearOptions();
            micList.AddOptions(options);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (micList.gameObject.activeSelf == true)
            {
                micList.gameObject.SetActive(false);
                sensSlider.gameObject.SetActive(false);
                sensLabel.gameObject.SetActive(false);
                sensValue.gameObject.SetActive(false);
                hueSlider.gameObject.SetActive(false);
                hueLabel.gameObject.SetActive(false);
                revertLabel.gameObject.SetActive(false);
                revertValue.gameObject.SetActive(false);
                revertSlider.gameObject.SetActive(false);
                bounceToggle.gameObject.SetActive(false);
            }
            else
            {
                micList.gameObject.SetActive(true);
                sensSlider.gameObject.SetActive(true);
                sensLabel.gameObject.SetActive(true);
                sensValue.gameObject.SetActive(true);
                hueSlider.gameObject.SetActive(true);
                hueLabel.gameObject.SetActive(true);
                revertLabel.gameObject.SetActive(true);
                revertValue.gameObject.SetActive(true);
                revertSlider.gameObject.SetActive(true);
                bounceToggle.gameObject.SetActive(true);
            }
        }
    }
    void MicValueChanged(Dropdown change)
    {
        micReference.microphoneInput = Microphone.Start(Microphone.devices[change.value], true, 999, 44100);
    }
    void SensSliderChanged(Slider change)
    {
        sensValue.text = change.value.ToString("F2");
    }
    void HueSliderValueChanged(Slider change)
    {
        mainCam.backgroundColor = Color.HSVToRGB(change.value, S, V);
    }
    void RevertSliderValueChanged(Slider change)
    {
        revertValue.text = change.value.ToString("F2");
    }
    void KeybindMenuClicked()
    {
        if (emote1Button.gameObject.activeSelf == true)
        {
            emote1Button.gameObject.SetActive(false);
            emote2Button.gameObject.SetActive(false);
            emote3Button.gameObject.SetActive(false);
            emote4Button.gameObject.SetActive(false);
            emote5Button.gameObject.SetActive(false);
            emoteResetButton.gameObject.SetActive(false);
        }
        else
        {
            emote1Button.gameObject.SetActive(true);
            emote2Button.gameObject.SetActive(true);
            emote3Button.gameObject.SetActive(true);
            emote4Button.gameObject.SetActive(true);
            emote5Button.gameObject.SetActive(true);
            emoteResetButton.gameObject.SetActive(true);
        }
    }

    IEnumerator Emote1ButtonClicked()
    {
        while (!Input.anyKey)
        {
            yield return null;
        }
        emoteBindings[0] = Input.inputString;
        emote1Button.GetComponentInChildren<Text>().text = "Emote 1: " + emoteBindings[0];
    }
    IEnumerator Emote2ButtonClicked()
    {
        while (!Input.anyKey)
        {
            yield return null;
        }
        emoteBindings[1] = Input.inputString;
        emote2Button.GetComponentInChildren<Text>().text = "Emote 2: " + emoteBindings[1];
    }
    IEnumerator Emote3ButtonClicked()
    {
        while (!Input.anyKey)
        {
            yield return null;
        }
        emoteBindings[2] = Input.inputString;
        emote3Button.GetComponentInChildren<Text>().text = "Emote 3: " + emoteBindings[2];
    }
    IEnumerator Emote4ButtonClicked()
    {
        while (!Input.anyKey)
        {
            yield return null;
        }
        emoteBindings[3] = Input.inputString;
        emote4Button.GetComponentInChildren<Text>().text = "Emote 4: " + emoteBindings[3];
    }
    IEnumerator Emote5ButtonClicked()
    {
        while (!Input.anyKey)
        {
            yield return null;
        }
        emoteBindings[4] = Input.inputString;
        emote5Button.GetComponentInChildren<Text>().text = "Emote 5: " + emoteBindings[4];
    }
    IEnumerator EmoteResetButtonClicked()
    {
        while (!Input.anyKey)
        {
            yield return null;
        }
        emoteBindings[5] = Input.inputString;
        emoteResetButton.GetComponentInChildren<Text>().text = "Reset: " + emoteBindings[5];
    }
}
