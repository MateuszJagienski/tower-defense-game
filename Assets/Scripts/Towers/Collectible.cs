using Assets.Scripts.Economy;
using UnityEngine;

namespace Assets.Scripts.Towers
{
    public class Collectible : MonoBehaviour
    {
        [SerializeField] private int gold;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float deathTime;

        private void Awake()
        {
            Invoke("OnDestroy", deathTime);
        }

        private void Update()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit, Mathf.Infinity, layerMask)) return;
            
            AddGold();
        }

        private void AddGold()
        {
            EconomySystem.Instance.IncreaseGold(gold);
            gameObject.SetActive(false);
        }
        private void OnDestroy()
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

    }
}
