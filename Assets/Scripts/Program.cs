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
    [SerializeField] private Slider numberOfPointsSlider;

    private void Awake()
    {
        numberOfPointsSlider.onValueChanged.AddListener(ValueChanged);
    }

    void Start()
    {
        Reset();
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

        float successRatePercentage = ((float)correctPoints / (float)points.Count) * 100.0f;
        successRateText.text = "Success Rate: " + successRatePercentage + "%";

        UpdatePerceptronInfo();
    }
    
    private void UpdatePerceptronInfo()
    {
        float[] weightsInfo = perceptron.GetWeightsInfo();
        perceptronInfo.text = "Perceptron Info: " + "\n- W0: " + weightsInfo[0] + "\n- W1: " + weightsInfo[1];
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

    public void ClearPoints()
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
    
    public void DeactivateButton(Button button)
    {
        button.interactable = false;
    }

    private void ValueChanged(float value)
    {
        numberOfPointsText.text = "Points: " + (int)value;
        numberOfPoints = (int)value;
    }
}