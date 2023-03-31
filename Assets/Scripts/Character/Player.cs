using Common;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(PlayerHealth))]
    public class Player : MonoBehaviour
    {
        public IHealth Health { get; private set; }

        private void Awake()
        {
            Health = GetComponent<PlayerHealth>();
        }
    }
}