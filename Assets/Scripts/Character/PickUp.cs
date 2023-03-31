using UnityEngine;

namespace Character
{
    public class PickUp : MonoBehaviour
    {
        [SerializeField] private PlayerWeapon _playerWeapon;
        
        private void OnTriggerEnter(Collider other)
        {
            Pick(other.gameObject);
        }

        private void Pick(GameObject item)
        {
            if (item.TryGetComponent(out Weapon.Weapon weapon) == false) return;
            
            if(weapon.Collider.isTrigger == false)
                _playerWeapon.SetWeapon(weapon);
        }
    }
}