using UnityEngine;

public class DoorController : MonoBehaviour
{
    private GameObject player;
    MonsterManager monsterManager;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        monsterManager = GetComponent<MonsterManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            UIOverlays.INSTANCE.ToggleEndDayIndicator(true);
            monsterManager.enabled = true;
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            UIOverlays.INSTANCE.ToggleEndDayIndicator(false);
            monsterManager.enabled = false;
        }   
    }

    void OnTriggerStay()
    {
        
    }
}
