using System;
using Assets.Scripts.Towers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI
{
    public class GameUI : MonoBehaviour
    {
        public UnityEvent<Tower> OnSelection;
        public event Action<Tower> SelectionChanged;
        [SerializeField]
        private LayerMask layerMask;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
    
            //  if (Input.GetMouseButton(0))
            //  {
            //      TrySelectTower();
            //  }

            // clicked once
            if (Input.GetMouseButtonDown(0))
            {
                TrySelectTower();
            }
        }

        private void TrySelectTower()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                Debug.Log(hit.collider);
                var tower = hit.collider.GetComponent<Tower>();
                if (tower == null) return;
                if (SelectionChanged != null) SelectionChanged(tower);
                OnSelection?.Invoke(tower);
            } 
            else if (!EventSystem.current.IsPointerOverGameObject())
            {
                SelectionChanged?.Invoke(null);
            }
        }

    }
}