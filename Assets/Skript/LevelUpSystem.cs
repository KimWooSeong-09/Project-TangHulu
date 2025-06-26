using Unity.VisualScripting;
using UnityEngine;

public class LevelUpSystem : MonoBehaviour
{
    [SerializeField] private float LevelCount = 1f;
    [SerializeField] private float currentAmount = 0f;
    [SerializeField] private float LevelUpAmount = 200f;
    public PlayerStatHandler _playerStat;
    public ChoiceManager _ChoiceManager;

    public void Start()
    {
        currentAmount = 0f;
        Debug.Log(currentAmount);
    }
    public void AddLevelAmount(float value)
    {
        currentAmount += value;
        
        while (currentAmount >= LevelUpAmount)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        Debug.Log("레벨업");
        _ChoiceManager.ShowRandomChoices();
        LevelCount ++;
        currentAmount -= LevelUpAmount;
        LevelUpAmount = LevelUpAmount * 1.5f;
        Debug.Log("레벨 업 후 현재 경헙치 : " + currentAmount);
        
    }
}