using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perceptron
{
    float[] weights = new float[2];
    float learningRate = 0.1f;

    public Perceptron()
    {
        //Initializing the weights randomly
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = Random.Range(-1.0f, 1.0f);
        }
    }

    public int Guess(float[] inputs)
    {
        float sum = 0;
        
        for (int i = 0; i < inputs.Length; i++)
        {
            sum += inputs[i] * weights[i];
        }

        int output = (int)Mathf.Sign(sum); //Activation Function

        return output;
    }

    public void Train(float[] inputs, int target)
    {
        int guess = Guess(inputs);
        int error = target - guess;

        //Adjusting the weights
        for (int i = 0; i < weights.Length; i++)
        {
            float deltaWeight = error * inputs[i] * learningRate;
            weights[i] += deltaWeight;
        }
    }

    public float[] GetWeightsInfo()
    {
        return weights;
    }
}
