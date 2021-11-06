using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Delu;

public class TestManager : MonoBehaviour
{
    [SerializeField] private Point pointGO;

    Program program;

    bool testMode = false;
    public bool TestMode { get => testMode; set => testMode = value; }

    private void Awake()
    {
        program = GetComponent<Program>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && testMode)
        {
            Vector2 pointPos = Utils.GetMouseWorldPos();

            if ((pointPos.x <= 5.0f && pointPos.x >= -5.0f) && (pointPos.y <= 5.0f && pointPos.y >= -5.0f))
            {
                Point point = Instantiate(pointGO, pointPos, Quaternion.identity);

                point.InitData(pointPos);

                point.SetGuessedLabel(program.GuessLabelFromPoint(point.GetCoordinates()));

                program.AddPoint(point);
            }
        }
    }
}
