using UnityEngine;
using Unity.Netcode;
using Assets.Scripts.Network;

namespace Assets.Scripts.Character
{
    public class PlayerShooting : NetworkBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject projectile;
        private Vector3 shootDirection;
        private float time = 0f;
        private GameObject currentProjectile;
        private void Update()
        {
            if (!IsOwner) return;
            if (player == null || projectile == null) return;
            if (Input.GetKeyDown(KeyCode.X) && time > 2f)
            {
                Destroy(currentProjectile);
                ShootServerRpc();
                Shoot2();
                time = -1 * Time.deltaTime;
            }
            time += Time.deltaTime;
        }

        [ServerRpc]
        private void ShootServerRpc()
        {
            Shoot1ClientRpc();
        }

        [ClientRpc]
        private void Shoot1ClientRpc()
        {
            if (!IsOwner) Shoot2();
        }

        private void Shoot2()
        { 
            currentProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
            currentProjectile.GetComponent<Projectile>().Init(new Vector3(1, 1, 1));
        }


    }
}