using UnityEngine;

public class SpawnZoneDarts : MonoBehaviour
{
   public  GameObject darts;

    private void OnTriggerExit(Collider other)
    {
        if (Physics.OverlapBox(transform.position, Vector3.one / 2).Length < 8)
        {
            Instantiate(darts, transform.position, Quaternion.identity);
        }
    }
}
