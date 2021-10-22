using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI : MonoBehaviour
{
    //UI elements
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
    public Button emote1Button, emote2Button, emote3Button, emote4Button, emote5Button, emoteResetButton;


    public Camera mainCam;
    public bool settingKey = false;
    public MicDetect micReference;
    public string bindValue = "9999";
    public List<string> emoteBindings = new List<string>();

    float H, S, V;

    // Start is called before the first frame update
    void Start()
    {
        //getting HSV values of camera's BG color
        Color.RGBToHSV(mainCam.backgroundColor, out H, out S, out V);

        //setting hue slider to match current camera BG color
        hueSlider.value = H;

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
            StartCoroutine("EmoteButtonClicked", EventSystem.current.currentSelectedGameObject);
        });
        emote2Button.onClick.AddListener(delegate
        {
            StartCoroutine("EmoteButtonClicked", EventSystem.current.currentSelectedGameObject);
        });
        emote3Button.onClick.AddListener(delegate
        {
            StartCoroutine("EmoteButtonClicked", EventSystem.current.currentSelectedGameObject);
        });
        emote4Button.onClick.AddListener(delegate
        {
            StartCoroutine("EmoteButtonClicked", EventSystem.current.currentSelectedGameObject);
        });
        emote5Button.onClick.AddListener(delegate
        {
            StartCoroutine("EmoteButtonClicked", EventSystem.current.currentSelectedGameObject);
        });
        emoteResetButton.onClick.AddListener(delegate
        {
            StartCoroutine("EmoteButtonClicked", EventSystem.current.currentSelectedGameObject);
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
                keybindMenu.gameObject.SetActive(false);
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
                keybindMenu.gameObject.SetActive(true);
            }
        }
    }

    void MicValueChanged(Dropdown change)
    {
        micReference.microphoneInput = Microphone.Start(Microphone.devices[change.value], true, 999, 44100);
    }
    public void MicValueChanged(int device)
    {
        micReference.microphoneInput = Microphone.Start(Microphone.devices[device], true, 999, 44100);
    }

    void SensSliderChanged(Slider change)
    {
        sensValue.text = change.value.ToString("F2");
    }

    void HueSliderValueChanged(Slider change)
    {
        mainCam.backgroundColor = Color.HSVToRGB(change.value, S, V);
    }
    public void HueSliderValueChanged(float hueIn)
    {
        mainCam.backgroundColor = Color.HSVToRGB(hueIn, S, V);
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

    IEnumerator EmoteButtonClicked(GameObject buttonObj)
    {   
        settingKey = true;
        buttonObj.GetComponent<Image>().color = new Color32(0xC4, 0xC4, 0xC4, 0xFF);
        int buttonNum = int.Parse(buttonObj.name);
        while (bindValue == "9999")
        {
            yield return null;
        }
        if (bindValue != "Escape")
        {
            emoteBindings[buttonNum] = bindValue;
            if(buttonNum == 5)
            {
                buttonObj.GetComponentInChildren<Text>().text = "Reset: " + emoteBindings[buttonNum];
            }
            else
            {
                buttonObj.GetComponentInChildren<Text>().text = "Emote " + (buttonNum + 1) + ": " + emoteBindings[buttonNum];
            }
        }
        bindValue = "9999";
        buttonObj.GetComponent<Image>().color = new Color32(0xFF, 0xFF, 0xFF, 0xFF);
        settingKey = false;
    }
}
