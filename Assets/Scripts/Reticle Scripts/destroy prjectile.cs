using UnityEngine;

public class destroyprjectile : MonoBehaviour
{
    private void Update()
    {
        if(this.transform.position.x < -10 || this.transform.position.x > 10)
        {
            Destroy(gameObject);
        }
        if(this.transform.position.y < -10 || this.transform.position.y > 10)
        {
            Destroy(gameObject);
        }
    }
}
