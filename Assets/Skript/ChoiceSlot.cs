using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChoiceSlot : MonoBehaviour
{
    public Image iconImage;
    public TMP_Text nameText;
    public TMP_Text descText;
    public Button selectButton;

    public void Setup(SkillItemData data, System.Action onClick)
    {
        Debug.Log("Setup Called: " + data.name);

        if (iconImage != null)
        {
            iconImage.gameObject.SetActive(true);
            iconImage.sprite = data.icon;
        }

        if (nameText != null)
        {
            nameText.gameObject.SetActive(true);
            nameText.text = data.name;
        }

        if (descText != null)
        {
            descText.gameObject.SetActive(true);
            descText.text = data.description;
        }

        if (selectButton != null)
        {
            selectButton.gameObject.SetActive(true);
            selectButton.onClick.RemoveAllListeners();
            selectButton.onClick.AddListener(() => onClick());
        }
        
        iconImage.sprite = data.icon;
        nameText.text = data.name;
        descText.text = data.description;
        selectButton.onClick.AddListener(() => onClick());
    }
}