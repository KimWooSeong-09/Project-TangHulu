using UnityEngine;

public class MangoProjectile : MonoBehaviour
{
    private float damage = 20f;
    public float lifetime = 5f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, lifetime); 
    }

    public void SetDamage(float dmg)
    {
        damage = dmg;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            OneEnemy oneEnemy = collision.gameObject.GetComponent<OneEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            if (oneEnemy != null)
            {
                oneEnemy.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}