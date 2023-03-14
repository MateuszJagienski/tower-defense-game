using System.Collections.Generic;
using Assets.Scripts.Economy;
using UnityEngine;

namespace Assets.Scripts.Towers
{
    public class TowerPlacement : MonoBehaviour
    {

        public float SphereRadius;

        [SerializeField]
        private LayerMask placementLayerMask;
        [SerializeField]
        private LayerMask collideLayers;

        [SerializeField] List<GameObject> towers;
        [SerializeField] List<GameObject> ghostTowers;

        private GameObject objectToSpawn;
        private GameObject ghostTower;
        private GameObject ghostTowerRange;
        private float range;
        // Start is called before the first frame update
        void Start()
        {

        }
        public void ChangeTower(int index)
        {
            DeactivateGhosts();
            objectToSpawn = towers[index];
            range = objectToSpawn.GetComponent<Tower>().Range;
            ghostTower = Instantiate(ghostTowers[index], Input.mousePosition, Quaternion.identity);
            // index 0 bo towerCopsule jest index 0 zmienic pozniej
            ghostTowerRange = Instantiate(ghostTowers[0], Input.mousePosition, Quaternion.identity);
        }

        // Update is called once per frame
        void Update()
        {
            if (objectToSpawn != null)
            {
                SpawnTower();
            }
        }

        void SpawnTower()
        {
/*        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }*/
            RaycastHit hit;

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, placementLayerMask, QueryTriggerInteraction.Ignore))
            {
                var x = hit.point.x;
                var z = hit.point.z;
                var y = hit.point.y;
                ghostTower.transform.position = new Vector3(x, y + 0.7f, z);
                ghostTowerRange.transform.position = new Vector3(x, y + 0.5f, z);
                ghostTowerRange.transform.localScale = new Vector3(range * 2, 0.1f, range * 2);
                if (Input.GetButtonDown("Fire1"))
                {
                    if (!CheckCollision())
                    {
                        EconomySystem.Instance.DecreaseGold(objectToSpawn.GetComponent<Tower>().PurchaseCost);
                        Spawn();
                    }
                }
            }
        }

        private bool CheckCollision()
        {
            return Physics.CheckSphere(ghostTower.transform.position, SphereRadius, collideLayers);
        }

        private void Spawn()
        {
            Instantiate(objectToSpawn, ghostTower.transform.position, Quaternion.identity);
            objectToSpawn = null;
            DeactivateGhosts();
        }

        private void DeactivateGhosts()
        {
            if (ghostTower != null || ghostTowerRange != null)
            {
                ghostTower.SetActive(false);
                Destroy(ghostTower);
                ghostTowerRange.SetActive(false);
                Destroy(ghostTowerRange);            
            }
        }
    }
}
