using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : MonoBehaviour
{
    [SerializeField] float fallSpeed;
    [SerializeField] int livesToAdd;

    private void Update()
    {
        FallDown();
    }

    void FallDown()
    {
        transform.Translate(Vector2.down * Time.deltaTime * fallSpeed);
    }

    public void ApplyPowerUp()
    {
        GameManager.instance.UpdateLiveNum(livesToAdd);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("The Void"))
        {
            Destroy(this.gameObject);
        }
    }
}
