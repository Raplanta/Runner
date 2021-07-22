using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private static Player _instancePlayer;

    [SerializeField] private Counter _count;
    [SerializeField] private Animator _runAnimation;
    [SerializeField] private float _speed;

    private bool _runAnimationBool;

    public Counter Count
    {
        get
        {
            return _count;
        }
    }

    public static Player Instance
    {
        get 
        {
            return _instancePlayer;
        }

        private set 
        {
            _instancePlayer = value;
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        var h = Input.GetAxis("Horizontal");

        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out hit, 0.6f))
        {
            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                _rigidbody.AddForce(new Vector3(0f, 450f, 0f));
            }

            _runAnimationBool = h==0? false : true;
            _runAnimation.SetBool("Run", _runAnimationBool);

            transform.rotation = Quaternion.Euler(0, -Mathf.Sign(h) * 90, 0);
            _rigidbody.velocity = new Vector3(h * _speed * Time.deltaTime, 0f, 0f);
        }
    }

    public void PlayerDie()
    {
        StartCoroutine(Fade());
    }

    private IEnumerator Fade() 
    {
        float sizeDelta = 3f;
        yield return new WaitWhile(() =>
        { 
            transform.localScale -= new Vector3(sizeDelta, sizeDelta, sizeDelta) * Time.deltaTime;
            return transform.localScale.x > 0.1f;
        } );

        Destroy(gameObject);
    }
}
