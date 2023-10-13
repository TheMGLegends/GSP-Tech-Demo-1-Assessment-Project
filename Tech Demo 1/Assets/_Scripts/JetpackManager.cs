using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JetpackManager : MonoBehaviour
{
    public static JetpackManager jetpackManager;

    [Header("Jetpack Settings:")]
    [SerializeField] private Image fuelBar;
    [SerializeField] private float fuelAmount;
    private float maxFuel;

    private void Awake()
    {
        jetpackManager = this;
    }

    private void Start()
    {
        maxFuel = fuelAmount;
    }

    public void DecreaseFuel(float fuelDecrease)
    {
        fuelAmount -= fuelDecrease;
        fuelBar.fillAmount = fuelAmount / maxFuel;
    }

    public void IncreaseFuel(float fuelIncrease)
    {
        fuelAmount += fuelIncrease;
        fuelAmount = Mathf.Clamp(fuelAmount, 0, maxFuel);
        fuelBar.fillAmount = fuelAmount / maxFuel;
    }

    public float ReturnFuelAmount()
    {
        return fuelAmount;
    }
}
