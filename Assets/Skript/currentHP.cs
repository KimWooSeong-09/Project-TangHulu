using UnityEngine;
using UnityEngine.UI;

public class currentHP : MonoBehaviour
{
    public PlayerStatHandler _playerStatHandler;
    private Slider _currentHpSlider;
    void Start()
    {
        _currentHpSlider = gameObject.GetComponent<Slider>();
    }
    void Update()
    {
        _currentHpSlider.maxValue = _playerStatHandler.maxHealth;
        _currentHpSlider.value = _playerStatHandler.currentHp;
    }
}
