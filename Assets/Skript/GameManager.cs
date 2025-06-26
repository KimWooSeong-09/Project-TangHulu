using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton
    {
        get => m_singlenton;
    }
    public static GameManager m_singlenton; 

    // 여러 오브젝트 불러오기
    public GoldManager goldManager;
    public LevelUpSystem levelSys;
    public PlayerMovement player;
    public RoundManager roundManager;
    public EnemySpawnManger enemySpawnManger;
    public GameObject enemyFolder;
    public GameObject projFolder;

    void Awake()
    {
        m_singlenton = this;
    }
}
