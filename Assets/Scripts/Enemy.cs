using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 2.3f;
    [SerializeField] private float _distance = 10f;
    [SerializeField] private float _lengthWay = 8;
    [SerializeField] private Transform rayPoint;
    [SerializeField] private float _acceleration = 1.2f;

    private Vector3 _startTransformPosition;
    private float[] posX;
    protected bool isDead = false;

    void Start()
    {
        _startTransformPosition = transform.position;
        posX = new float[2]{ _startTransformPosition.x, _lengthWay };
        StartCoroutine(Patrol());
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.name == "Player")
        {
            Vector3 enemyPosition = gameObject.gameObject.transform.position;
            Vector3 playerPosition = other.gameObject.transform.position;
            Vector3 deltaPosition = enemyPosition - playerPosition;
            if (Mathf.Abs(deltaPosition.y) > Mathf.Abs(deltaPosition.x) && playerPosition.y > enemyPosition.y) 
            {
                isDead = true;
                StartCoroutine(EnemyDie());
            } 
            else if (!isDead)
            {
                Player.Instance.PlayerDie();
            }
        }
    }

    public IEnumerator EnemyDie() 
    {
        float sizeDelta = 10f;
        yield return new WaitWhile(() =>
        { 
            transform.localScale -= new Vector3(sizeDelta, sizeDelta, sizeDelta) * Time.deltaTime;
            return transform.localScale.x > 0.1f;
        } );

        Destroy(gameObject);
    }

    public IEnumerator Patrol()
    { 
        float delta = 0f;
        int index = 1;
        float eps = 0.1f;

        while (!FindPlayer())
        {
            delta += Time.deltaTime * speed;
            transform.position -= (transform.position.x - posX[index]) * Time.deltaTime * Vector3.right * speed;
            float del = transform.position.x - posX[index];
            if (Mathf.Abs(del) < eps)
            {
                index = index == 1 ? 0 : 1;
            }
            transform.rotation = Quaternion.Euler(0, -Mathf.Sign(del) * 90, 0);

            yield return null;
        }

        StartCoroutine(Hunt());
    }

    public IEnumerator Hunt()
    {
        while (Vector3.Distance(transform.position, Player.Instance.transform.position) < 8f)
        {
            if (Player.Instance != null)
            {
                transform.position = Vector3.Lerp(transform.position, Player.Instance.transform.position, 0.3f * Time.deltaTime * speed * _acceleration);
            }

            yield return null;
        }

        StartCoroutine(Patrol());
    }

    bool FindPlayer()
    {
        RaycastHit hit;

        Ray ray = new Ray(rayPoint.position, Vector3.left);

        if (Physics.Raycast(ray, out hit, _distance))
        {
            if (hit.transform.tag == "Player")
                return true;
        }

        return false;
    }

}
