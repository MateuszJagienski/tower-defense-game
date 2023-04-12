using Assets.Scripts.Economy;
using UnityEngine;

namespace Assets.Scripts.Towers
{
    public class Collectible : MonoBehaviour
    {
        [SerializeField] private int gold;
        [SerializeField] private LayerMask layerMask;

        private void Start()
        {
            
        }

        private void Update()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, layerMask,
        QueryTriggerInteraction.Ignore)) return;
            Debug.Log(hit.collider);
            if (hit.collider.TryGetComponent<Collectible>(out var c))
            {
                Debug.Log($"{hit.collider}");
            }
        }

        private void OnMouseEnter()
        {
            Debug.Log("Mouse enter");
            AddGold();
        }

        private void OnMouseOver()
        {
            Debug.Log("Mouse over");
        }

        private void AddGold()
        {
            EconomySystem.Instance.IncreaseGold(gold);
            gameObject.SetActive(false);
            Destroy(gameObject, 2);
        }
    }
}
