using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradesHandler : MonoBehaviour
{
    public GameObject upgrades;

    public TextMeshProUGUI candyCanesCounter;

    public int candyCaneAmount = 0;

    public int HealthUpgradeCurrentLevel, ShieldUpgradeCurrentLevel, TimeUpgradeCurrentLevel;

    public List<int> healthUpgradeCost = new();
    public List<int> shieldUpgradeCost = new();
    public List<int> timeUpgradeCost = new();

    public List<string> healthUpgradeText = new();
    public List<string> shieldUpgradeText = new();
    public List<string> timeUpgradeText = new();

    public bool healthMaxed, shieldMaxed, timeMaxed;

    public TextMeshProUGUI healthLevelText, shieldLevelText, timeLevelText;

    public TextMeshProUGUI healthCostShower, shieldCostShower, timeCostShower;

    public TextMeshProUGUI healthUpgradeTextShower, shieldUpgradeTextShower, timeUpgradeTextShower;

    void Start()
    {
        upgrades.SetActive(PlayerPrefs.GetInt("UpgradesUnlocked", 0) == 1);

        candyCaneAmount = PlayerPrefs.GetInt("CandyCanes", 0);

        candyCanesCounter.text = candyCaneAmount.ToString();


        HealthUpgradeCurrentLevel = PlayerPrefs.GetInt("HealthUpgradeCurrentLevel", 1);
        ShieldUpgradeCurrentLevel = PlayerPrefs.GetInt("ShieldUpgradeCurrentLevel", 0);
        TimeUpgradeCurrentLevel = PlayerPrefs.GetInt("TimeUpgradeCurrentLevel", 0);

        healthMaxed = healthUpgradeCost.Count == HealthUpgradeCurrentLevel;
        
        shieldMaxed = shieldUpgradeCost.Count == ShieldUpgradeCurrentLevel;
        
        timeMaxed = timeUpgradeCost.Count == TimeUpgradeCurrentLevel;

        healthLevelText.text = $"{HealthUpgradeCurrentLevel}/{healthUpgradeCost.Count}";
        shieldLevelText.text = $"{ShieldUpgradeCurrentLevel}/{shieldUpgradeCost.Count}";
        timeLevelText.text = $"{TimeUpgradeCurrentLevel}/{timeUpgradeCost.Count}";
    }

    void Update()
    {
        healthCostShower.text = healthMaxed ? "--" : healthUpgradeCost[HealthUpgradeCurrentLevel].ToString();
        shieldCostShower.text = shieldMaxed ? "--" : shieldUpgradeCost[ShieldUpgradeCurrentLevel].ToString();
        timeCostShower.text = timeMaxed ? "--" : timeUpgradeCost[TimeUpgradeCurrentLevel].ToString();

        candyCanesCounter.text = PlayerPrefs.GetInt("CandyCanes", 0).ToString();
    }

    void SaveLevels()
    {
        PlayerPrefs.SetInt("HealthUpgradeCurrentLevel", HealthUpgradeCurrentLevel);
        PlayerPrefs.SetInt("ShieldUpgradeCurrentLevel", ShieldUpgradeCurrentLevel);
        PlayerPrefs.SetInt("TimeUpgradeCurrentLevel", TimeUpgradeCurrentLevel);

        healthUpgradeTextShower.text = healthUpgradeText[HealthUpgradeCurrentLevel];
        shieldUpgradeTextShower.text = shieldUpgradeText[ShieldUpgradeCurrentLevel];
        timeUpgradeTextShower.text = timeUpgradeText[TimeUpgradeCurrentLevel];
    }

    public void HealthUpgrade()
    {
        if (candyCaneAmount >= healthUpgradeCost[HealthUpgradeCurrentLevel] && ! healthMaxed)
        {
            candyCaneAmount -= healthUpgradeCost[HealthUpgradeCurrentLevel];

            PlayerPrefs.SetInt("CandyCanes",
                PlayerPrefs.GetInt("CandyCanes", 0) - healthUpgradeCost[HealthUpgradeCurrentLevel]);

            HealthUpgradeCurrentLevel++;
            
            healthMaxed = healthUpgradeCost.Count == HealthUpgradeCurrentLevel;

            healthLevelText.text = $"{HealthUpgradeCurrentLevel}/{healthUpgradeCost.Count}";

            SaveLevels();
        }
    }

    public void ShieldUpgrade()
    {
        if (candyCaneAmount >= shieldUpgradeCost[ShieldUpgradeCurrentLevel] && !shieldMaxed)
        {
            candyCaneAmount -= shieldUpgradeCost[ShieldUpgradeCurrentLevel];

            PlayerPrefs.SetInt("CandyCanes",
                PlayerPrefs.GetInt("CandyCanes", 0) - shieldUpgradeCost[ShieldUpgradeCurrentLevel]);

            ShieldUpgradeCurrentLevel++;

            shieldMaxed = shieldUpgradeCost.Count == ShieldUpgradeCurrentLevel;

            shieldLevelText.text = $"{ShieldUpgradeCurrentLevel}/{shieldUpgradeCost.Count}";
           
            SaveLevels();
        }
    }

    public void TimeUpgrade()
    {
        if (candyCaneAmount >= timeUpgradeCost[TimeUpgradeCurrentLevel] && !timeMaxed)
        {
            candyCaneAmount -= timeUpgradeCost[TimeUpgradeCurrentLevel];

            PlayerPrefs.SetInt("CandyCanes",
                PlayerPrefs.GetInt("CandyCanes", 0) - timeUpgradeCost[TimeUpgradeCurrentLevel]);

            TimeUpgradeCurrentLevel++;

            timeMaxed = timeUpgradeCost.Count == TimeUpgradeCurrentLevel;

            timeLevelText.text = $"{TimeUpgradeCurrentLevel}/{timeUpgradeCost.Count}";
            
            SaveLevels();
        }
    }
}
