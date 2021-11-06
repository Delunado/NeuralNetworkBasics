using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    private float x;
    private float y;
    private int label;

    private SpriteRenderer sprite;
    [SerializeField] private SpriteRenderer spriteCorrectMark;

    public int Label { get => label; }

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void InitData(Vector2 worldPos)
    {
        x = worldPos.x;
        y = worldPos.y;

        if (x > y)
        {
            label = 1;
            sprite.color = Color.white;
        }
        else
        {
            label = -1;
            sprite.color = Color.black;
        }

        transform.position = new Vector3(x, y, 0.0f);
    }

    public void SetGuessedLabel(int label)
    {
        if (label == 1)
        {
            sprite.color = Color.white;
        } else if (label == -1)
        {
            sprite.color = Color.black;
        }

        BrainCorrect(label == Label);
    }

    public float[] GetCoordinates()
    {
        float[] coordinates = { x, y };
        return coordinates;
    }

    public void BrainCorrect(bool isCorrect)
    {
        spriteCorrectMark.color = isCorrect ? Color.green : Color.red;
    }
}