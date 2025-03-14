using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;

public class Sliding : IMinigame
{
    [SerializeField] private Transform gameTransform;

    [SerializeField] private Transform prefab;

    [SerializeField] private float resetTime;

    [SerializeField] private int points;

    private List<Transform> pieces;

    private bool reset = false;

    private float gapWidth = 0.5f; 

    private int emptyLocation;

    private int size;

    void Start()
    {
        pieces = new List<Transform>();

        size = Random.Range(2, 5);

        CreateGamePieces();
    }

    void Update()
    {
        if (reset) return; 

        if (Check())
        {
            MinigameComplete(true); 

            StartCoroutine(WaitShuffle());
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D collider = Physics2D.OverlapPoint(mouseWorldPosition);

            if (collider != null)
            {
                Transform hit = collider.transform;

                for (int i = 0; i < pieces.Count; i++)
                {
                    if (pieces[i] == hit.transform)
                    {
                        if (canSwap(i, -size, size)) break;
                        if (canSwap(i, size, size)) break;
                        if (canSwap(i, -1, 0)) break;
                        if (canSwap(i, 1, size - 1)) break;
                    }
                }
            }
        }
    }

    private void CreateGamePieces()
    {
        float width = 1 / (float)size;

        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                Transform piece = Instantiate(prefab, gameTransform);

                pieces.Add(piece);

                piece.localPosition = new Vector3(-1 + (2 * width * col) + width,
                                                   1 - (2 * width * row) - width, 0);

                piece.localScale = ((2 * width) - gapWidth) * Vector3.one;

                piece.name = $"{(row * size) + col}";

                if ((row == size - 1) && (col == size - 1))
                {
                    emptyLocation = (size * size) - 1;

                    piece.gameObject.SetActive(false);
                }

                else
                {
                    Mesh mesh = piece.GetComponent<MeshFilter>().mesh;

                    Vector2[] uv = new Vector2[4];

                    float gap = gapWidth / 2; 

                    float widthCol = width * col;
                    float widthColPlus = width * (col + 1);

                    float widthRow = width * row;
                    float widthRowPlus = width * (row + 1);

                    uv[0] = new Vector2(widthCol + gap, 1 - widthRowPlus - gap);
                    uv[1] = new Vector2(widthColPlus - gap, 1 - widthRowPlus - gap);
                    uv[2] = new Vector2(widthCol + gap, 1 - widthRow + gap);
                    uv[3] = new Vector2(widthColPlus - gap, 1 - widthRow + gap);

                    mesh.uv = uv; 
                }
            }
        }
    }

    private bool canSwap(int i, int offset, int colCheck)
    {
        if ((i % size != colCheck) && (i + offset == emptyLocation))
        {     
            (pieces[i], pieces[i + offset]) = (pieces[i + offset], pieces[i]);
            
            (pieces[i].localPosition, pieces[i + offset].localPosition) = 
                ((pieces[i + offset].localPosition, pieces[i].localPosition));
            
            emptyLocation = i;

            return true;
        }

        return false;
    }

    private bool Check()
    {
        for (int i = 0; i < pieces.Count; i++)
        {
            if (pieces[i].name != $"{i}")
                return false;
        }

        return true;
    }

    private IEnumerator WaitShuffle()
    {
        reset = true; 

        yield return new WaitForSeconds(resetTime);

        Shuffle();

        reset = false;
    }

    private void Shuffle()
    {
        int count = 0;

        int last = 0;

        while (count < (size * size * size))
        {
            int n = Random.Range(0, size * size);

            if (n == last) continue;

            last = emptyLocation;

            if (canSwap(n, -size, size) || canSwap(n, size, size) ||
                canSwap(n, -1, 0) || canSwap(n, 1, size - 1)) 
                count++;
        }
    }

    public override int CalculateScore()
    {
        return points;
    }
}



