using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class ChoiceManager : MonoBehaviour
{
    public List<SkillItemData> allChoices; 
    public Transform choiceContainer;
    public GameObject choiceSlotPrefab;
    public PlayerStatHandler playerStatHandler;
    public PlayerAttack _playerAttack;
    public MangoRainSystem _mangoRain;
    public experienceOb _ExperienceOb;
    public GoldManager _goldManager;

    public int maxDuplicates = 5;

    private Dictionary<string, int> skillCounts = new();
    private List<SkillItemData> ownedItems = new();
    public void ShowRandomChoices()
    {
        Time.timeScale = 0;
        
        foreach (Transform child in choiceContainer) Destroy(child.gameObject);

        List<SkillItemData> pool = new(allChoices);
        List<SkillItemData> selected = new();

        while (selected.Count < 3)
        {
            var pick = pool[Random.Range(0, pool.Count)];
            selected.Add(pick);
        }

        foreach (var item in selected)
        {
            GameObject go = Instantiate(choiceSlotPrefab, choiceContainer);
            go.SetActive(true);
            go.GetComponent<ChoiceSlot>().Setup(item, () => OnSelect(item));
        }
        
        choiceContainer.gameObject.SetActive(true);
    }

    void OnSelect(SkillItemData data)
    {
        if (data.isSkill)
        {
            if (!skillCounts.ContainsKey(data.name)) skillCounts[data.name] = 0;
            if (skillCounts[data.name] < maxDuplicates)
            {
                skillCounts[data.name]++;

                if (data.name == "딸기 탕후루")
                {
                    playerStatHandler.IncreaseStat(PlayerStatHandler.PlayerStat.Damage, 5f);
                    _playerAttack.Increase();
                }

                if (data.name == "망고 탕후루")
                {
                    var mangoRain = FindObjectOfType<MangoRainSystem>();
                    if (mangoRain == null)
                    {
                        Debug.Log("응 실행 안대ㅗ");
                    }
                    else
                    {
                        Debug.Log("실행 됌");
                        StartCoroutine(FindObjectOfType<MangoRainSystem>().RainRoutine());    
                    }
                    
                }

                if (data.name == "샤인머스켓 탕후루")
                {
                    Debug.Log("샤인머스켓 탕후루 선택 됨.");
                }

                if (data.name == "블랙 사파이어 탕후루")
                {
                    Debug.Log("블랙 사파이어 탕후루 선택 됨.");
                }

                if (data.name == "경험치 획득량 증가")
                {
                    _ExperienceOb.Increase();
                }

                if (data.name == "미다스의 손")
                {
                    _goldManager.IncreaseGold(0f, true);
                }
                
                if (data.name == "운동화")
                {
                    playerStatHandler.IncreaseStat(PlayerStatHandler.PlayerStat.Speed, 0f);
                }

                if (data.name == "알약")
                {
                    playerStatHandler.Heal();
                }
            }
        }
        else    
        {
            ownedItems.Add(data);
        }
        choiceContainer.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
