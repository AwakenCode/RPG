using Data.Weapon;
using Infrastructure.Factory;
using Service.Data;
using Service.Input;
using UnityEngine;
using Zenject;

namespace Weapon
{
    public class FireArm : Weapon, IReloadable
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private WeaponType _type;

        private FireArmData _data;
        private IGameFactory _gameFactory;
        private IInputService _inputService;
        private float _elapsedTime;
        
        public uint CurrentAmmoInMagazine { get; private set; }

        [Inject]
        private void Construct(IGameFactory gameFactory, IDataProvider dataProvider)
        {
            _gameFactory = gameFactory;
            _data = (FireArmData) dataProvider.GetWeaponData(_type);
            Init(_data);
        }
        
        private void Update()
        {
            _elapsedTime += Time.deltaTime;
        }

        public override async void Fire()
        {
            if (_elapsedTime < _data.FiringRate) return;
            if (CurrentAmmoInMagazine == 0) return;
            
            await _gameFactory.CreateBullet(_data.AmmunitionType, _shootPoint);
            CurrentAmmoInMagazine--;
            _elapsedTime = 0;
        }

        public void Reload()
        {
            CurrentAmmoInMagazine += _data.MagazineSize - CurrentAmmoInMagazine;
            _elapsedTime = -_data.ReloadTime;
        }
    }
}