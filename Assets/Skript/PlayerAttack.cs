using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject strawberryPrefab;
    public float shootForce = 70f;
    public float detectionRange = 100f;
    public GameObject CloneFolder;
    public float SpeedAttack = 0.6f;

    private PlayerStatHandler statHandler;
    public PlayerStatHandler _playerStatHandler;

    private Transform firePoint;

    public void Increase()
    {
        SpeedAttack -= 0.1f;
    }
    void Start()
    {

        // FirePoint 오브젝트 생성
        GameObject firePointObj = new GameObject("FirePoint");
        firePointObj.transform.SetParent(transform);
        firePointObj.transform.localPosition = new Vector3(0, 0.7f, 0.5f); // 가슴팍 위치
        firePoint = firePointObj.transform;

        InvokeRepeating("AttackPlayer", 0f, SpeedAttack);
    }   
    public void AttackPlayer()
    {
        GameObject closestEnemy = FindClosestEnemy();
        if (closestEnemy == null || firePoint == null) return;

        // 가슴팍 위치에서 딸기 생성
        GameObject strawberry = Instantiate(strawberryPrefab, firePoint.position, Quaternion.identity);
        strawberry.transform.SetParent(CloneFolder.transform);

        Vector3 direction = (closestEnemy.transform.position - firePoint.position).normalized;

        Rigidbody rb = strawberry.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.AddForce(direction * shootForce, ForceMode.VelocityChange);

        }
        if (rb == null)
        {
            Debug.LogWarning("딸기에 Rigidbody가 없음!");
        }
        
        StrawberryDamage damageScript = strawberry.GetComponent<StrawberryDamage>();
        damageScript._playerStatHandler = this._playerStatHandler;
        if (damageScript != null && _playerStatHandler != null)
        {
            damageScript._playerStatHandler.baseDamage = Mathf.RoundToInt(_playerStatHandler.baseDamage);
        }
    }


    GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Target");
        GameObject closest = null;
        float minDist = detectionRange;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = enemy;
            }
        }

        return closest;
    }
    
    
}