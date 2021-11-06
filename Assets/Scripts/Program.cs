using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Program : MonoBehaviour
{
    [SerializeField] private int numberOfPoints = 100;
    [SerializeField] private Point pointGO;
    List<Point> points;

    Perceptron perceptron;

    int iterationNumber;
    float successRate;

    [SerializeField] private TextMeshProUGUI iterationText;
    [SerializeField] private TextMeshProUGUI successRateText;
    [SerializeField] private TextMeshProUGUI numberOfPointsText;
    [SerializeField] private TextMeshProUGUI perceptronInfo;
    [SerializeField] private TextMeshProUGUI testButtonText;
    [SerializeField] private Slider numberOfPointsSlider;

    TestManager testManager;

    private void Awake()
    {
        testManager = FindObjectOfType<TestManager>();
    }

    void Start()
    {
        Reset();
    }

    public void Reset()
    {
        iterationNumber = 0;
        iterationText.text = "Iteration Nº: " + iterationNumber;

        successRate = 0.0f;
        successRateText.text = "Success Rate: " + successRate + "%";

        perceptron = new Perceptron();

        ClearPoints();

        UpdatePerceptronInfo();
    }

    private void UpdatePerceptronInfo()
    {
        float[] weightsInfo = perceptron.GetWeightsInfo();
        perceptronInfo.text = "Perceptron Info: " + "\n- W0: " + weightsInfo[0] + "\n- W1: " + weightsInfo[1];
    }

    public void GenerateTrainingData()
    {
        ClearPoints();

        for (int i = 0; i < numberOfPoints; i++)
        {
            Point point = Instantiate(pointGO);
            points.Add(point);

            point.InitData(new Vector2(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f)));
        }
    }

    public void TrainPerceptron()
    {
        int correctPoints = 0;

        foreach (Point p in points)
        {
            perceptron.Train(p.GetCoordinates(), p.Label);

            int guess = perceptron.Guess(p.GetCoordinates());

            bool correctGuess = p.Label == guess;

            p.BrainCorrect(correctGuess);

            if (correctGuess)
            {
                correctPoints++;
            }
        }

        iterationNumber++;
        iterationText.text = "Iteration Nº: " + iterationNumber;

        float successRatePercenteage = ((float)correctPoints / (float)points.Count) * 100.0f;
        successRateText.text = "Success Rate: " + successRatePercenteage + "%";

        UpdatePerceptronInfo();
    }

    private void ClearPoints()
    {
        if (points != null)
        {
            foreach (Point p in points)
            {
                Destroy(p.gameObject);
            }

            points.Clear();
        }
        else
        {
            points = new List<Point>();
        }
    }

    public void EnterTestMode()
    {
        ClearPoints();

        if (testManager.TestMode)
        {
            testButtonText.text = "Test";
            testManager.TestMode = false;
        } else
        {
            testButtonText.text = "Stop Test";
            testManager.TestMode = true;
        }
    }

    public void AddPoint(Point point)
    {
        points.Add(point);
    }

    public int GuessLabelFromPoint(float[] pointCoordinates)
    {
        return perceptron.Guess(pointCoordinates);
    }

    public void ActivateButton(Button button)
    {
        button.interactable = true;
    }

    private void Update()
    {
        numberOfPointsText.text = "Points: " + (int)numberOfPointsSlider.value;
        numberOfPoints = (int)numberOfPointsSlider.value;
    }
}