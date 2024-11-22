using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] Toggle toggle;
    [SerializeField] Slider slider;
    [SerializeField] Scrollbar scrollbar;
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] TMP_Text text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button.onClick.AddListener(OnBtnClkEvent);
    }

    // Update is called once per frame
    void Update()
    {
        print("Toggle Button: " + toggle.isOn);
        print("Slider Value: " + slider.value);
        print("Scrollbar Value: " + scrollbar.value);
        print("Dropdown Value: " + dropdown.value);
        print("InputField Value: " + inputField.text);
        text.text = inputField.text;
    }

    void OnBtnClkEvent()
    {
        print("¹öÆ° Å¬¸¯µÊ!");
    }
}
