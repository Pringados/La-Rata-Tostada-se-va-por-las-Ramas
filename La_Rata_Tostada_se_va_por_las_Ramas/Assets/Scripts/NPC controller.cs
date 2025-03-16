using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System;

public class NPCcontroller : MonoBehaviour
{
    [SerializeField]
    GameObject NPCSprite;
    [SerializeField]
    GameObject text;
    [SerializeField]
    GameObject button;
    NPCData data;
    int actDiag, actLine = 0;
    // Start is called before the first frame update
    void Start()
    {
        //ponemos un maravilloso npc en su sitio
        data = MapManager.instance.getNPCData();
        NPCSprite.GetComponent<Image>().sprite = data.sprite;

        //seleccionamos un dialogo aleatoriamente
        actDiag = UnityEngine.Random.Range(0, data.dialogue.Count());

        //la ponemos
        text.GetComponent<TMP_Text>().text = data.dialogue[actDiag].array[actLine].text;
        Debug.Log("npcInstanciado");

        button.GetComponent<Button>().onClick.AddListener(onButtonPush);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //si hacen click, al siguiente
    private void onButtonPush()
    {
        //comprobación de si queda algo q decir
        actLine++;
        if(actLine >= data.dialogue[actDiag].array.Count())
        {
            Debug.Log("a cambiar de escena");
            GameManager.instance.ChangeScene(MapManager.instance.getDestino());
        }
        else
        {
            text.GetComponent<TMP_Text>().text = data.dialogue[actDiag].array[actLine].text;
        }

    }
}
