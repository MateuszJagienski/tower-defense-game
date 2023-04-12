using Assets.Scripts.Enemies;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Towers
{
    public class IncomeGenerator : TowerController
    {
        [SerializeField] private GameObject collectible;
        protected override IEnumerator Fire()
        {
            while (WaveManager.Instance.IsRunning)
            {
                var position = new Vector3(transform.position.x + Random.Range(-2, 2), transform.position.y, 
                    transform.position.z + Random.Range(-2, 2));
                Instantiate(collectible, position, Quaternion.identity);
                yield return new WaitForSeconds(1 / Tower.AttackSpeed);
            }


        }
    }
}
