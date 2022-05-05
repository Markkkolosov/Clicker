using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Game : MonoBehaviour
{
    [SerializeField] float Score;
    public float[] CostInt;
    private float ClickScore = 1;
    public float[] CostBonus;
    private float TotalBonus;
    

    public GameObject ShopPan;
    public GameObject BonusPan;
    public GameObject SettingsPan;
    public GameObject AchievementsPan;
    public GameObject ColorPickerPan;

    public Text[] CostText;
    public Text ScoreText;

    private Save sv = new Save();

    [Header("Достижения")]
    private int Achievement1Max;

    private bool isAcievement1 = true;
    private bool isAcievement2 = true;
    private bool isAchievement2Get = false;
    private bool isAcievement3 = true;
    private bool isAchievement3Get = false;

    public Text[] AchievementsText;
    public Text[] AchievementsCost;
    public Text Achievement1NameText;


    private void Awake()
    {
        if(PlayerPrefs.HasKey("SV"))
        {
            sv = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("SV"));
            Score = sv.Score;
            ClickScore = sv.ClickScore;
            Achievement1Max = sv.Achievement1Max;
            isAcievement1 = sv.isAcievement1;

            isAcievement2 = sv.isAcievement2;
            isAchievement2Get = sv.isAchievement2Get;

            isAchievement3Get = sv.isAchievement3Get;
            isAcievement3 = sv.isAcievement3;

            for (int i = 0; i < 1; i++)
            {
                CostBonus[i] = sv.CostBonus[i];
                TotalBonus += sv.CostBonus[i];
            }

            for (int i = 0; i < 2; i++)
            {
                CostInt[i] = sv.CostInt[i];
                CostText[i].text = sv.CostInt[i] + "$";
            }
        }
    }


    private void Start()
    {
        StartCoroutine(BonusShop());

        DateTime dt = new DateTime(sv.Date[0], sv.Date[1], sv.Date[2], sv.Date[3], sv.Date[4], sv.Date[5]);
        TimeSpan ts = DateTime.Now - dt;
        Score += (int)ts.TotalSeconds * TotalBonus;
        print("Вы заработали: " + ((int)ts.TotalSeconds * TotalBonus) + "$");
    }

    public void OnClickButton()
    {
        Score += ClickScore;
        if (isAcievement1 == true && Achievement1Max < 100)
        {
            Achievement1Max++;
        }
    }

    private void Update()
    {
        ScoreText.text = Score + "$";
        Achievement1NameText.text = "Нажмите " + Achievement1Max + "/100 раз";

        if (isAcievement1 == false)
        {
            AchievementsCost[0].text = "Получено";
        }

        if(Achievement1Max == 100)
        {
            AchievementsText[0].text = "Выполнено";
        }

        if (isAchievement2Get == true)
        {
            AchievementsCost[1].text = "Получено";
        }

        if (isAcievement2 == false)
        {
            AchievementsText[1].text = "Выполнено";
        }

        if (isAchievement3Get == true)
        {
            AchievementsCost[2].text = "Получено";
        }

        if (isAcievement3 == false)
        {
            AchievementsText[2].text = "Выполнено";
        }

        ScoreText.text = ReductionNum._ReductionNum(Score) + "$";

        for(int i = 0; i < CostText.Length; i++)
        {
            CostText[i].text = ReductionNum._ReductionNum(CostInt[i]) + "$";
        }
    }

    public void ShowAndHideShopPan()
    {
        ShopPan.SetActive(!ShopPan.activeSelf);
    }
    public void ShowAndHideBonusPan()
    {
        BonusPan.SetActive(!BonusPan.activeSelf);
    }

    public void ShowAndHideSettingsPan()
    {
        SettingsPan.SetActive(!SettingsPan.activeSelf);
    }
    public void ShowAndHideAchievementsPan()
    {
        AchievementsPan.SetActive(!AchievementsPan.activeSelf);
    }

    public void ShowAndHideColorPickerPan()
    {
        ColorPickerPan.SetActive(!ColorPickerPan.activeSelf);
    }


    public void OnClickBuyLevel()
    {
        if (Score >= CostInt[0])
        {
            Score -= CostInt[0];
            CostInt[0] *= 2;
            ClickScore *= 2;
            CostText[0].text = CostInt[0] + "$";
            isAcievement2 = false;
        }
    }

    public void OnClickBuyBonusShop()
    {
        if (Score >= CostInt[1])
        {
            Score -= CostInt[1];
            CostInt[1] *= 2;
            CostBonus[0] += 2;
            CostText[1].text = CostInt[1] + "$";
            isAcievement3 = false;
        }
    }

    IEnumerator BonusShop()
    {
        while (true)
        {
            Score += CostBonus[0];
            yield return new WaitForSeconds(1);
        }
    }
#if UNITY_ANDROID && !UNITY_EDITOR
    private void OnApplicationPause(bool pause)
    {
        if(pause)
            {
                sv.Score = Score;
                sv.ClickScore = ClickScore;
                sv.CostBonus = new float[1];
                sv.CostInt = new float[2];
                sv.Achievement1Max = Achievement1Max;
                sv.isAcievement1 = isAcievement1;
                sv.isAcievement2 = isAcievement2;
                sv.isAchievement2Get = isAchievement2Get;

                sv.isAchievement3Get = isAchievement3Get;
                sv.isAcievement3 = isAcievement3;

                for(int i = 0; i < 1; i++)
                {
                    sv.CostBonus[i] = CostBonus[i];
                }

                for (int i = 0; i < 2; i++)
                {
                    sv.CostInt[i] = CostInt[i];
                }

                sv.Date[0] = DateTime.Now.Year; sv.Date[1] = DateTime.Now.Month; sv.Date[2] = DateTime.Now.Day; sv.Date[3] = DateTime.Now.Hour; sv.Date[4] = DateTime.Now.Minute; sv.Date[5] = DateTime.Now.Second;

                PlayerPrefs.SetString("SV", JsonUtility.ToJson(sv));
    }
    }
#else
    private void OnApplicationQuit()
    {
        sv.Score = Score;
        sv.ClickScore = ClickScore;
        sv.CostBonus = new float[1];
        sv.CostInt = new float[2];
        sv.Achievement1Max = Achievement1Max;
        sv.isAcievement1 = isAcievement1;
        sv.isAcievement2 = isAcievement2;
        sv.isAchievement2Get = isAchievement2Get;

        sv.isAchievement3Get = isAchievement3Get;
        sv.isAcievement3 = isAcievement3;

        for(int i = 0; i < 1; i++)
        {
            sv.CostBonus[i] = CostBonus[i];
        }

        for (int i = 0; i < 2; i++)
        {
            sv.CostInt[i] = CostInt[i];
        }

        sv.Date[0] = DateTime.Now.Year; sv.Date[1] = DateTime.Now.Month; sv.Date[2] = DateTime.Now.Day; sv.Date[3] = DateTime.Now.Hour; sv.Date[4] = DateTime.Now.Minute; sv.Date[5] = DateTime.Now.Second;

        PlayerPrefs.SetString("SV", JsonUtility.ToJson(sv));
    }
#endif



    public void OnClickAchievement1Button()
    {
        if(isAcievement1 == true && Achievement1Max == 100)
        {
            Score += 5000;
            isAcievement1 = false;
        }
    }

    public void OnClickAchievement2Button()
    {
        if(isAcievement2 == false)
        {
            Score += 3000;
            isAcievement2 = false;
            isAchievement2Get = true;
        }
    }

    public void OnClickAchievement3Button()
    {
        if (isAcievement3 == false)
        {
            Score += 6000;
            isAcievement3 = false;
            isAchievement3Get = true;
        }
    }
}

[Serializable]
public class Save
{
    public float Score;
    public float ClickScore;
    public float[] CostInt;
    public float[] CostBonus;
    public int[] Date = new int[6];

    public bool isAcievement1;
    public bool isAcievement2;
    public bool isAchievement2Get;
    public bool isAcievement3;
    public bool isAchievement3Get;

    public int Achievement1Max;
}
