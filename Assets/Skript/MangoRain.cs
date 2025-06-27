using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MangoRainSystem : MonoBehaviour
{
    public GameObject mangoPrefab; // 떨어뜨릴 망고 프리팹
    public float interval = 10f;
    public float spawnHeightOffset = 20f;
    public int maxTargets = 10;
    public float damage = 20f;
    public LayerMask enemyLayer;

    private Transform player;

    public void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    public IEnumerator RainRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            SpawnMangoRain();
        }
    }

    public void SpawnMangoRain()
    {
        Debug.Log("망고 스폰 됌");
        List<Transform> enemies = FindNearestEnemies(player.position, maxTargets);

        foreach (Transform enemy in enemies)
        {
            Vector3 spawnPos = enemy.position + Vector3.up * spawnHeightOffset;
            Debug.Log("망고 스폰 위치" + spawnPos);
            GameObject mango = Instantiate(mangoPrefab, spawnPos, Quaternion.identity);
            MangoProjectile mp = mango.GetComponent<MangoProjectile>();
            mp.SetDamage(damage);
        }
    }

    List<Transform> FindNearestEnemies(Vector3 center, int count)
    {
        Collider[] hits = Physics.OverlapSphere(center, 100f, enemyLayer);
        List<Transform> enemyList = new();

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
                enemyList.Add(hit.transform);
        }

        enemyList.Sort((a, b) =>
            Vector3.Distance(center, a.position).CompareTo(Vector3.Distance(center, b.position)));

        return enemyList.GetRange(0, Mathf.Min(count, enemyList.Count));
    }
}