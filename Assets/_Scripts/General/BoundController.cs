using UnityEngine;

public class BoundController : MonoBehaviour
{
    public Transform resetPosition;

    public void OnTriggerExit2D(Collider2D other)
    {
        other.transform.position = resetPosition.position;
    }
}
