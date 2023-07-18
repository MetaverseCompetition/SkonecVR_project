using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector2.zero;
        StartCoroutine(Open());
    }

    private IEnumerator Open()
    {
        yield return new WaitForSeconds(0.3f);
        GetComponent<AudioSource>().Play();
        transform.LeanScale(Vector2.one, 0.8f);
      
        StartCoroutine(Close());
    }

    private IEnumerator Close()
    {
        yield return new WaitForSeconds(3f);

        transform.LeanScale(Vector2.zero, 1f).setEaseInBack();

        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
