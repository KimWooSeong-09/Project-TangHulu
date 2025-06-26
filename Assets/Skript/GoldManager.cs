using UnityEngine;
using TMPro;

public class GoldManager : MonoBehaviour
{
    public float currentGold = 0f;
    public TextMeshProUGUI goldText;
    public float starAmount = 1f;
    public float IncraseGold = 1f;

    public void Start()
    {
        goldText.text = currentGold.ToString() + "\ud83e\ude99";
    }
    public void IncreaseGold(float goldMount, bool isSelect)
    {
        if (isSelect)
        {
            IncraseGold += IncraseGold * 0.1f;
        }

        if (!isSelect)
        {
            currentGold += goldMount * IncraseGold;
        }
    }
}
