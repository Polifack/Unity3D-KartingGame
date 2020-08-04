using System;
using UnityEngine;


[RequireComponent(typeof(AudioListener))]
public class AudioSpectrum : MonoBehaviour
{
    public int spectrumLength = 256;
    public FFTWindow fftWindow;
    public float denormalizationValue = 100;

    private float[] spectrum;
    
    //Value that will contain the current frequency value of the spectrum.
    public static float currentSpectrumValue { get; private set;}

    private void Start()
    {
        if (spectrumLength % 2 != 0) throw new Exception("Error > Spectrum length must be a power of two");
        spectrum = new float[spectrumLength];
    }

    void Update()
    {
        AudioListener.GetSpectrumData(spectrum, 0, fftWindow);
        if (spectrum != null && spectrum.Length > 0)
        {
            currentSpectrumValue = spectrum[0] * denormalizationValue;
        }
    }
}