using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNPC", menuName = "ScriptableObjects/NPCData", order = 2)]

public class NPCData : ScriptableObject
{
    [System.Serializable]
    public class DialogueLine
    {
        public string text;
        public string fmodEventPath;
    }

    [System.Serializable]
    public class DialoguePath
    {
        public DialogueLine[] array;
    }

    public DialoguePath[] dialogue;

    public Sprite sprite;

    // Siguiente nodo que se devolverá del camino de diálogo activo
    private int dialogueIndex = 0;

    // Camino de diálogo actualmente seleccionado
    private int currentDialoguePath = 0;

    public string GetDialogue()
    {
        DialogueLine line = dialogue[currentDialoguePath].array[dialogueIndex];
        //
        // PLAY line.fmodEventPath in an event emitter
        //
        dialogueIndex++;
        return line.text;
    }

    public void ResetDialogue()
    {
        dialogueIndex = 0;
        currentDialoguePath++;
        currentDialoguePath %= dialogue.Length;
    }
}
