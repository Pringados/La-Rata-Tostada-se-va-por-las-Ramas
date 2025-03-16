using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChangeCursor : MonoBehaviour
{
    [SerializeField] private Texture2D cursor; 
    void Start()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
    }
}
