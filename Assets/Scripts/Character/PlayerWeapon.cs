using System.Threading.Tasks;
using Service.Data;
using Service.Input;
using UnityEngine;
using Weapon;
using Zenject;

namespace Character
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private Transform _holder; 
        
        private Weapon.Weapon _currentWeapon;

        private IDataProvider _dataProvider;

        [Inject]
        private void Construct(Task<PlayerInput> input, IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
            Init(input);
        }

        private async void Init(Task<PlayerInput> inputService)
        {
            await inputService;
            inputService.Result.AttackButtonClicked += Shoot;
            inputService.Result.ReloadButtonClicked += Reload;
        }

        public void SetWeapon(Weapon.Weapon weapon)
        {
            if (_currentWeapon != null)
            {
                Destroy(_currentWeapon.gameObject);
                _currentWeapon = null;
            }
            
            weapon.Collider.isTrigger = true;
            weapon.Rigidbody.useGravity = false;
            weapon.Rigidbody.isKinematic = true;
            var weaponTransform = weapon.transform;
            var weaponData = _dataProvider.GetWeaponTransformData(weapon.Data.Type);
            
            weaponTransform.SetParent(_holder);
            weaponTransform.localPosition = weaponData.Position;
            weaponTransform.localRotation = weaponData.Rotation;
            _currentWeapon = weapon;
        }
        
        private void Shoot()
        {
            _currentWeapon?.Fire();
        }
        
        private void Reload()
        {
            if(_currentWeapon is IReloadable reloadable) 
                reloadable.Reload();
        }
    }
}