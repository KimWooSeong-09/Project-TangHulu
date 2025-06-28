using UnityEngine;

public class StrawberryDamage : MonoBehaviour
{
    public PlayerStatHandler _playerStatHandler;

    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            Enemy enemyHealth = collision.gameObject.GetComponent<Enemy>();
            OneEnemy oneEnemyHealth = collision.gameObject.GetComponent<OneEnemy>();
            
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(_playerStatHandler.baseDamage);
            }

            else if (oneEnemyHealth != null)
            {
                oneEnemyHealth.TakeDamage(_playerStatHandler.baseDamage);
            }

            Destroy(gameObject);
        }
    }
}