using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    [SerializeField] GameObject target;

    NavMeshAgent agent;
    EnemyHealth em;
    const string PLAYER_TAG = "Player";

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        em = GetComponent<EnemyHealth>();
    }

    private void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PLAYER_TAG))
        {
            PlayerHealth hitPlayer = other.GetComponent<PlayerHealth>();
            if (hitPlayer != null && PlayerHealth.IsPlayerAlive)
            {
                hitPlayer.TakeDamage(4);
            }

            em?.SelfDestroy();
        }
    }
}
