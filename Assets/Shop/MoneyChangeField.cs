using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyChangeField : MonoBehaviour
{
    public Image field;
    public Text moneyText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMoneyChange(bool isPlus, int amount)
    {
        StartCoroutine(ShowMoneyChangeInformation(isPlus, amount));
    }

    IEnumerator ShowMoneyChangeInformation(bool isPlus, int amount)
    {
        string sign;

        if (isPlus) sign = "+";
        else sign = "-";

        gameObject.SetActive(true);
        field.color = new Color(field.color.r, field.color.g, field.color.b, 255);
        moneyText.color = new Color(moneyText.color.r, moneyText.color.g, moneyText.color.b, 255);

        moneyText.text = sign + string.Format("{0:#,0}", amount);

        yield return new WaitForSeconds(1);
        while (true)
        {
            field.color = new Color(field.color.r, field.color.g, field.color.b, field.color.a - (Time.deltaTime));
            moneyText.color = new Color(moneyText.color.r, moneyText.color.g, moneyText.color.b, moneyText.color.a - (Time.deltaTime));
            if (field.color.a <= 0)
                field.color = new Color(field.color.r, field.color.g, field.color.b, 0);
            if (moneyText.color.a <= 0)
            {
                moneyText.color = new Color(moneyText.color.r, moneyText.color.g, moneyText.color.b, 0);
                break;
            }
        }
        yield return null;
    }
}
