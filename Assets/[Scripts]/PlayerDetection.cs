using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public LayerMask PlayerColisionLayerMask;
    public Collider2D colliderName;
    public bool PlayerDetected;
    public bool LOS;
    public Transform PlayerTransform;
    public Vector2 PlayerDirectionVector;
    public float playerDirection;
    public float enemyDirection;
    // Start is called before the first frame update
    void Start()
    {
        PlayerDirectionVector = Vector2.zero;
        playerDirection = 0.0f;
        PlayerTransform = FindObjectOfType<PlayerBehaviour>().transform;
        LOS = false;
        PlayerDetected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerDetected)
        {
            var hits = Physics2D.Linecast(transform.position, PlayerTransform.position, PlayerColisionLayerMask);

            colliderName = hits.collider;
            LOS = (hits.collider.gameObject.name == "Player");

            PlayerDirectionVector = PlayerTransform.position - transform.position;
            playerDirection = (PlayerDirectionVector.x > 0) ? 1.0f : -1.0f;
            enemyDirection = GetComponentInParent<EnemyController>().direction.x;

            LOS = (hits.collider.gameObject.name == "Player") && (playerDirection == enemyDirection);
        }

        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Player")
        {
            PlayerDetected = true;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = (LOS) ? Color.green : Color.red;
        if(PlayerDetected)
        {
            Gizmos.DrawLine(transform.position, PlayerTransform.position);
        }

            Gizmos.color = (PlayerDetected ? Color.red : Color.green);
        Gizmos.DrawWireSphere(transform.position, 1.5f);
    }
}
