using Unity.Netcode;
using UnityEngine;

namespace Assets.Scripts.Network
{
    public class PlayerTransport : NetworkBehaviour
    {

        private NetworkVariable<Vector3> playerPosition;

        private void Awake()
        {
            playerPosition = new NetworkVariable<Vector3>(writePerm: NetworkVariableWritePermission.Owner);
        }

        public override void OnNetworkSpawn()
        {
            playerPosition.OnValueChanged += OnValueChanged;
            if (!IsOwner) Destroy(GetComponent<CharacterController>());
        }

        public override void OnNetworkDespawn()
        {
            playerPosition.OnValueChanged -= OnValueChanged;
        }


        private void OnValueChanged(Vector3 oldPosition, Vector3 newPosition)
        {
            transform.position = newPosition;
        }


        private void Update()
        {
            if (IsOwner) playerPosition.Value = transform.position;
            else transform.position = playerPosition.Value;
        }

/*        private struct PlayerState : INetworkSerializable
        {
            public Vector3 Position;
        }

            public Quaternion Rotation;

            public void NetworkSerialize(NetworkSerializer serializer)
            {
                serializer.Serialize(ref Position);
            }

            public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
            {
                throw new System.NotImplementedException();
            }
        }*/

    }
}
