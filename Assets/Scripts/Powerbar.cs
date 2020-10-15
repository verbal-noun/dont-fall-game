using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Powerbar : MonoBehaviour {


    private Slider powerbarDisplay;

    public float power = 100;

    private int powerPercentage = 100;
    public float minimumPower = 0;

    public float maximumPower = 100;

    public int lowPower = 33;

    public int highPower = 66;
    public Color highPowerColor = new Color(0.35f, 1f, 0.35f);
    public Color mediumPowerColor = new Color(0.9450285f, 1f, 0.4481132f);
    public Color lowPowerColor = new Color(1f, 0.259434f, 0.259434f);

    private void Start()
    {
        powerbarDisplay = GetComponent<Slider>();
    
        powerbarDisplay.minValue = minimumPower;
        powerbarDisplay.maxValue = maximumPower;

        UpdatePower();
    }

    public void SetMaxValue(float value){
        maximumPower = value;
        powerbarDisplay.maxValue = value;
    }
    public void SetLowHighColor(float lp, float hp){
        this.lowPower =(int) Mathf.Round(lp);
        this.highPower =(int) Mathf.Round(hp);
    }

    private void Update()
    {
        powerPercentage = (int) (Mathf.Round(power));

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
        if (powerPercentage <= lowPower && power >= minimumPower && transform.Find("Bar").GetComponent<Image>().color != lowPowerColor)
        {
            ChangePowerbarColor(lowPowerColor);
        }
        else if (powerPercentage <= highPower && power > lowPower)
        {
            lerpedColorValue = (float) ((powerPercentage) - lowPower) / (highPower - lowPower);
            ChangePowerbarColor(Color.Lerp(lowPowerColor, mediumPowerColor, lerpedColorValue));
        }
        else if (powerPercentage > highPower && power <= maximumPower)
        {
            lerpedColorValue = (float) ((powerPercentage) - highPower) / (maximumPower - highPower);
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