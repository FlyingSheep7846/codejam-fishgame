using UnityEngine;

public class FishingSpot : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private float rotation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            FishingManager.INSTANCE.CanFish(true, rotation);
            UIOverlays.INSTANCE.ToggleFishIndicator(true);
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            FishingManager.INSTANCE.CanFish(false, rotation);
            UIOverlays.INSTANCE.ToggleFishIndicator(false);
        }
    }

}
