using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Delu;
using TMPro;

public class TestManager : MonoBehaviour
{
    [SerializeField] private Point pointGO;
    [SerializeField] private TextMeshProUGUI testButtonText;

    Program program;

    bool testMode;

    public bool TestMode
    {
        get => testMode;
        set => testMode = value;
    }

    private void Awake()
    {
        program = GetComponent<Program>();
    }

    public void EnterTestMode()
    {
        program.ClearPoints();

        testMode = !testMode;
        testButtonText.text = testMode ? "Stop Test" : "Test";
    }

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