using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCcontroller : MonoBehaviour
{
    [SerializeField]
    GameObject NPCSprite;
    [SerializeField]
    GameObject text;
    [SerializeField]
    GameObject button;
    NPCData data;
    // Start is called before the first frame update
    void Start()
    {
        //ponemos un maravilloso npc en su sitio
        data = MapManager.instance.getNPCData();
        NPCSprite.GetComponent<Image>().sprite = data.sprite;
        text.GetComponent<TMP_Text>().text = "multiplicate por 0";//data.dialogue[0].array[0].text;
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
        Debug.Log("a cambiar de escena");
        GameManager.instance.ChangeScene(MapManager.instance.getDestino());
        Debug.Log("supuestamente hemos cambiado la escena jefe");

    }
}
