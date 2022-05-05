using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ReductionNum
{
    private static string[] letters = new[]
    {
        "",
        "K",
        "M",
        "B",
        "T"
    };

    public static string _ReductionNum(float num)
    {
        if (num == 0) return "0";

        num = Mathf.Round(num);

        int i = 0;

        while(i + 1 < letters.Length && num >= 1000f)
        {
            num /= 1000f;
            i++;
        }

        return num.ToString(format: "#.##") + letters[i];
    }
}
