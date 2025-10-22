using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyPatrol : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform[] patrolPoints;
    public int targetPoint;
    public float speed;
 

   
    
    void Start()
    {
        targetPoint = 0;
       
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == patrolPoints[targetPoint].position)
        {
            IncreaseTargetInt();
        }
        {
            if (transform.position == patrolPoints[targetPoint].position)
            {
                targetPoint = 0;
            }
        }
        Vector2 direction = patrolPoints[targetPoint].position - transform.position;
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[targetPoint].position, speed * Time.deltaTime);

        // Flip enemy depending on movement direction
        if (direction.x > 0)
            transform.rotation = Quaternion.Euler(0, -180, 0); // Facing right
        else if (direction.x < 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);  // Facing left
    }

    void IncreaseTargetInt()
    {
        targetPoint++;
        if (targetPoint >= patrolPoints.Length)
        {
            targetPoint = 0;
        }
    }


}
