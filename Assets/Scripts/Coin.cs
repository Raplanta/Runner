using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Coin : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0, 2.0f, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponent<Collider>().enabled = false;
            StartCoroutine(Fade());
        }
    }

    IEnumerator Fade()
    {
        float sizeDelta = 3f;
        yield return new WaitWhile(() =>
        { 
            transform.localScale -= new Vector3(sizeDelta, sizeDelta, sizeDelta) * Time.deltaTime;
            return transform.localScale.x > 0.1f;
        } );
        
        Player.Instance.Count.AddCoints(1);
        Destroy(gameObject);
    }
}
