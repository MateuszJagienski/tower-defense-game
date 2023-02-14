using System.Collections.Generic;
using UnityEngine;

public class Targetter : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> targets = new List<GameObject>();
    [SerializeField]
    private Tower tower;

    public void SetRange(float radius)
    {
        GetComponent<SphereCollider>().radius = radius;
    }

    private void OnTriggerEnter(Collider other)
    {
        targets.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (targets.Contains(other.gameObject))
        {
            targets.Remove(other.gameObject);
        }
    }

    public List<GameObject> GetTargets()
    {
        return targets;
    }

    public void RemoveTarget(GameObject targetToRemove)
    {
        if (targets.Contains(targetToRemove))
        {
            targets.Remove(targetToRemove);
        }
    }
}
