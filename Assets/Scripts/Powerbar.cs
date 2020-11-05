using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Powerbar : MonoBehaviour
{


    [SerializeField]
    private Slider powerbarDisplay;

    public float power = 50;

    private float minimumPower = 0;
    private float maximumPower = 100;
    private int lowPower = 33;
    private int highPower = 66;
    public Color highPowerColor = new Color(0.35f, 1f, 0.35f);
    public Color mediumPowerColor = new Color(0.9450285f, 1f, 0.4481132f);
    public Color lowPowerColor = new Color(1f, 0.259434f, 0.259434f);

    private void Start()
    {

        UpdatePower();
    }

    public void SetMaxValue(float value)
    {
        maximumPower = value;
        powerbarDisplay.maxValue = value;
        this.lowPower = (int)Mathf.Round(value * 1/3);
        this.highPower = (int)Mathf.Round(value * 2/3);
    }
    private void Update()
    {

        if (power < minimumPower)
        {
            power = minimumPower;
        }

        else if (power > maximumPower)
        {
            power = maximumPower;
        }
    }

    public void UpdatePower()
    {
        float lerpedColorValue;
        if (power <= lowPower && power >= minimumPower && transform.Find("Bar").GetComponent<Image>().color != lowPowerColor)
        {
            ChangePowerbarColor(lowPowerColor);
        }
        else if (power <= highPower && power > lowPower)
        {
            lerpedColorValue = ((power) - lowPower) / (highPower - lowPower);
            ChangePowerbarColor(Color.Lerp(lowPowerColor, mediumPowerColor, lerpedColorValue));
        }
        else if (power > highPower && power <= maximumPower)
        {
            lerpedColorValue = ((power) - highPower) / (maximumPower - highPower);
            ChangePowerbarColor(Color.Lerp(mediumPowerColor, highPowerColor, lerpedColorValue));
        }

        powerbarDisplay.value = power;
    }

    public void GainPower(float amount)
    {
        power += amount;
        UpdatePower();
    }


    public void ChangePowerbarColor(Color colorToChangeTo)
    {
        transform.Find("Bar").GetComponent<Image>().color = colorToChangeTo;
    }

    public void SetPower(float value)
    {
        power = value;
        UpdatePower();
    }
}