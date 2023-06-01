using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupTextController : MonoBehaviour
{
    public Rigidbody rb;
    public float popForse;
    
    public void PopText()
    {
        rb.AddForce(Vector3.up * popForse, ForceMode.Impulse);
        StartCoroutine(Deactivate());
    }

    private IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(2f);
        rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }

}
