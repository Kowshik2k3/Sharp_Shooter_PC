using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int health = 3;
    [SerializeField] GameObject explosionFx;
    [SerializeField] Vector3 explosionOffset;

    private void Update()
    {
        if (health <= 0)
        {
            SelfDestroy();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"Enemy {gameObject.name} health: {health}");
    }

    public void SelfDestroy()
    {
        if (explosionFx != null)
        {
            Instantiate(explosionFx, transform.position + explosionOffset, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
