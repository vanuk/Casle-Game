using System.Collections;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    int i = 0;
    public GameObject instrument;
    public GameObject Enemy;
    public float speed;
    public float checkRadius;
    public float AttackRadius;
    public bool shouldRotate;
    public LayerMask whatIsPlayer;
    private Transform target;
    private Rigidbody2D rb;
    private Vector2 movement;
    public Vector3 dir;

    private bool isInChaseRange;
    private bool isInAttackRange;
     private Coroutine attackRoutine;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       // FindNearestPlayer();
       // FindNearestPoint();
        attackRoutine = StartCoroutine(AttackRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer);
        isInAttackRange = Physics2D.OverlapCircle(transform.position, AttackRadius, whatIsPlayer);

        if (target != null)
        {
            dir = target.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            dir.Normalize();
            movement = dir;
        }
    }

    private void FixedUpdate()
    { 
        if (isInChaseRange && !isInAttackRange && i==0 || i==5)
        {
            FindNearestPlayer();

            MoveCharacter(movement);
        }

        if (i == 5)
        {
           //StopCoroutine(attackRoutine);
            FindNearestPoint();
            //MoveCharacter(movement);
             // Скидання лічильника
        }
    }

    IEnumerator AttackRoutine()
    {
        while (true)
        {
            if (isInAttackRange && i<5)
            {
               // StartCoroutine(AttackRoutine());
                Farm();
                Debug.Log(i+" 1111");
            }
            yield return new WaitForSeconds(1f); // Очікування одну секунду перед наступною атакою
        }
    }

    private void Farm()
    {
       
        // Перевірка, чи об'єкт вже включений, якщо так, вимикаємо його, якщо ні - включаємо
        instrument.SetActive(!instrument.activeSelf);
        
        i++;

        // Перевірка, чи вже виконано дію 5 разів
        
    }
private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Door"))
    {
        i = 0;
        Debug.Log(i);
    }
}

    private void MoveCharacter(Vector2 dir)
    {
        rb.MovePosition((Vector2)transform.position + (dir * speed * Time.deltaTime));
    }

    private void FindNearestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
        {
            float shortestDistance = Mathf.Infinity;
            GameObject nearestPlayer = null;
            foreach (GameObject player in players)
            {
                float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
                if (distanceToPlayer < shortestDistance)
                {
                    shortestDistance = distanceToPlayer;
                    nearestPlayer = player;
                }
            }
            target = nearestPlayer.transform;
        }
    }
    private void FindNearestPoint()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Door");
        if (players.Length > 0)
        {
            float shortestDistance = Mathf.Infinity;
            GameObject nearestPlayer = null;
            foreach (GameObject player in players)
            {
                float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
                if (distanceToPlayer < shortestDistance)
                {
                    shortestDistance = distanceToPlayer;
                    nearestPlayer = player;
                }
            }
            target = nearestPlayer.transform;
        }
    }
}
