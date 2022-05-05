using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FCP_ExampleScript : MonoBehaviour
{
    public FlexibleColorPicker fcp;
    public Image imgSprite;

    public Color bgColor;

    private void Awake()
    {
        Load();
        fcp.color = bgColor;
    }

    private void Update() {

        imgSprite.color = fcp.color;
    }

    public void OnClickExit()
    {
        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("bgColorR", fcp.color.r);
        PlayerPrefs.SetFloat("bgColorG", fcp.color.g);
        PlayerPrefs.SetFloat("bgColorB", fcp.color.b);
    }

    private void Load()
    {
        if (PlayerPrefs.HasKey("bgColorR"))
        {
            bgColor.r = PlayerPrefs.GetFloat("bgColorR");
            bgColor.g = PlayerPrefs.GetFloat("bgColorG");
            bgColor.b = PlayerPrefs.GetFloat("bgColorB");
        }
    }


}
