using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Chat : MonoBehaviour
{
    [SerializeField]
    private string message = "Kill me using a [weapon]";

    [Header("CONFIGURATION")]
    [SerializeField]
    private float typeSpeed = 0.1f;
    [SerializeField]
    private float showTime = 2f;

    [Header("COMPONENTS")]
    [SerializeField]
    private GameObject backgroundGameObject;
    [SerializeField]
    private TextMeshProUGUI textField;

    private void Start()
    {
        backgroundGameObject.SetActive(false);
    }

    public void ShowUp(string weapon)
    {
        backgroundGameObject.SetActive(true);

        textField.text = "";

        string fullMessage = message.Replace("[weapon]", weapon);
        char[] charArray = fullMessage.ToCharArray();

        StartCoroutine(TypeChat(charArray));
    }



    private IEnumerator WaitThenHide()
    {
        yield return new WaitForSeconds(showTime);

        backgroundGameObject.SetActive(false);
    }



    private IEnumerator TypeChat(char[] charArray, int index = 0)
    {
        textField.text += charArray[index];

        yield return new WaitForSeconds(typeSpeed);

        index++;
        if(index >= charArray.Length)
        {
            StartCoroutine(WaitThenHide());
        }
        else
        {
            StartCoroutine(TypeChat(charArray, index));
        }

    }
}
