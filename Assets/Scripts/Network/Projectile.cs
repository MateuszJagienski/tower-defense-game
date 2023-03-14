using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Network
{
    public class Projectile : MonoBehaviour
    {
        private Vector3 prSpeed;

        private void Update()
        {
            if (prSpeed != Vector3.zero) transform.position += prSpeed * Time.deltaTime;
        }

        public void Init(Vector3 speed)
        {
            prSpeed = speed;
        }
    }
}
