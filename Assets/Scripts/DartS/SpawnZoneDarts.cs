using UnityEngine;

public class SpawnZoneDarts : MonoBehaviour
{
   public  GameObject darts;

    private void OnTriggerExit(Collider other)
    {
        foreach (var dart in Physics.OverlapBox(transform.position, Vector3.one / 2))
        {
            Debug.Log(dart.gameObject.name);
        }
        if (Physics.OverlapBox(transform.position, Vector3.one / 2).Length < 8)
        {
            Instantiate(darts);
        }
    }
}
