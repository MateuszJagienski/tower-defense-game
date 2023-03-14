
using Assets.Scripts.Enemies;
using UnityEngine;

namespace Assets.Scripts.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource popSound;
        private void Start()
        {
            EnemyController.OnEnemyDamaged += PlayPopSound;
        }

        void PlayPopSound(EnemyController enemyController)
        {
            var sound = popSound;
            sound.Play();
            Debug.Log("Play pop sound");
        }

    }
}