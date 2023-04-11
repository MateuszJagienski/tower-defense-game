using System.Collections.Generic;
using Assets.Scripts.Economy;
using UnityEngine;

namespace Assets.Scripts.Towers
{
    public class TowerPlacement : MonoBehaviour
    {

        private float sphereRadius = 2; // change to tower sphere radius

        [SerializeField] private LayerMask placementLayerMask;
        [SerializeField] private LayerMask collideLayers;
        [SerializeField] private List<GameObject> towers;
        [SerializeField] private List<GameObject> ghostTowers;

        private GameObject objectToSpawn;
        private GameObject ghostTower;
        private GameObject ghostTowerRange;
        private float range;


        public void ChangeTower(int index)
        {
            Debug.Log($"placement tower change tower");
            DeactivateGhosts();
            objectToSpawn = towers[index];
            range = objectToSpawn.GetComponent<Tower>().Range;
            ghostTower = Instantiate(ghostTowers[index], Input.mousePosition, Quaternion.identity);
            // index 0 bo towerCopsule jest index 0 zmienic pozniej xD
            ghostTowerRange = Instantiate(ghostTowers[0], Input.mousePosition, Quaternion.identity);
        }

        // Update is called once per frame
        private void Update()
        {
            if (objectToSpawn != null)
            {
                SpawnTower();
            }
        }

        private void SpawnTower()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log($"ray {ray}");
            if (!Physics.Raycast(ray, out var hit, Mathf.Infinity, placementLayerMask,
                    QueryTriggerInteraction.Ignore)) return;
            
            var x = hit.point.x;
            var z = hit.point.z;
            var y = hit.point.y;
            Debug.Log($"x {x}, y {y}, z {z}");
            ghostTower.transform.position = new Vector3(x, y + 0.7f, z);
            ghostTowerRange.transform.position = new Vector3(x, y + 0.5f, z);
            ghostTowerRange.transform.localScale = new Vector3(range * 2, 0.1f, range * 2);
            
            if (!Input.GetButtonDown("Fire1") || CheckCollision()) return;
                
            EconomySystem.Instance.DecreaseGold(objectToSpawn.GetComponent<Tower>().PurchaseCost);
            Spawn();
        }

        private bool CheckCollision()
        {
            return Physics.CheckSphere(ghostTower.transform.position, sphereRadius, collideLayers);
        }

        private void Spawn()
        {
            Instantiate(objectToSpawn, ghostTower.transform.position, Quaternion.identity);
            objectToSpawn = null;
            DeactivateGhosts();
        }

        private void DeactivateGhosts()
        {
            if (ghostTower != null)
            {
                ghostTower.SetActive(false);
                Destroy(ghostTower);
            }
            if (ghostTowerRange == null) return;
            
            ghostTowerRange.SetActive(false);
            Destroy(ghostTowerRange);
        }
    }
}
