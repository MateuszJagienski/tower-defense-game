using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class GameUI : MonoBehaviour
{

    public static event Action<Tower> selectionChanged;

    [SerializeField]
    private LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            TrySelectTower();
        }
    }

    private void TrySelectTower()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            Debug.Log(hit.collider);
            var tower = hit.collider.GetComponent<Tower>();
            if (tower != null)
            {
                selectionChanged(tower);
            }
        } 
        else if (!EventSystem.current.IsPointerOverGameObject())
        {
            selectionChanged(null);
        }
    }
}