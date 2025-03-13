using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KeyCodeCard : MonoBehaviour
{
    [SerializeField] Text cardCode;

    [SerializeField] Text cardInput;

    [SerializeField] float resetTime;

    private bool reset = false;

    private bool correct; 

    string letters = "ABC"; 

    private void OnEnable()
    {
        cardInput.text = string.Empty;

        NewCode(); 
    }

    private void NewCode()
    {
        string code = string.Empty;

        code += letters[Random.Range(0, letters.Length)];
        code += Random.Range(0, 4);

        cardCode.text = code;

        correct = false; 
    }

    public void OnButtonClick(string value)
    {
        if (reset) return;

        cardInput.text += value; 

        if (cardInput.text == cardCode.text)
        {
            correct = true; 

            cardInput.text = "CORRECT";

            StartCoroutine(ResetCode()); 
        }

        else if (cardCode.text.Length <= cardInput.text.Length)
        {
            cardInput.text = "FAILED";

            StartCoroutine(ResetCode());
        }
    }

    private IEnumerator ResetCode()
    {
        reset = true;

        yield return new WaitForSeconds(resetTime);

        if (correct) NewCode();

        cardInput.text = string.Empty; 

        reset = false;
    }
}
