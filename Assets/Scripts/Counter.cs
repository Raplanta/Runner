using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public Text CountCoint; 
    private int Coin;

    public void AddCoints(int cnt)
    {
        Coin += cnt;
        CountCoint.text = Coin.ToString();
    }
}
