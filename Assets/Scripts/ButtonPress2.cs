using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ButtonPress2 : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private int increaseAmount = 1;
    [SerializeField] private Canvas canvas1;
    [SerializeField] private Canvas canvas2;
    [SerializeField] private MonoBehaviour targetScript;
    [SerializeField] private MonoBehaviour targetScript1;
    [SerializeField] private TextMeshProUGUI correct;

 
    private Button button;

    private void Start()
    {
        // Get reference to the button component
        button = GetComponent<Button>();

        // Attach the click listener to the button
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        // Increase the value of the text
        int currentValue = int.Parse(textComponent.text);
        int newValue = currentValue + increaseAmount;
        textComponent.text = newValue.ToString();
        canvas2.enabled = true;
        correct.enabled = true;

        // Invoke the DisableTargetScript method after 2 seconds
        Invoke("DisableTargetScript", 2f);
    }

    private void DisableTargetScript()
    {
        // Disable the target script
        targetScript.enabled = true;
        targetScript1.enabled = true;
        canvas1.enabled = false;
        canvas2.enabled = false;
        correct.enabled = false;
    }
}
