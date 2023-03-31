using Data.Weapon;
using Enemy;
using Service.Asset;
using System;
using System.Threading.Tasks;
using Character;
using Infrastructure.Pool;
using Service.Data;
using Service.Input;
using UI;
using UnityEngine;
using Weapon;
using Weapon.Ammunition;
using Zenject;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IDataProvider _dataProvider;
        private readonly DiContainer _container;

        private BulletPool _bulletPool;
        private PlayerInput _inputService;
        
        public GameFactory(IAssetProvider assetProvider, IDataProvider dataProvider, DiContainer container)
        {
            _assetProvider = assetProvider;
            _dataProvider = dataProvider;
            _container = container;
        }

        public async void Initialize()
        {
            _bulletPool = await CreateBulletPool();
            
            foreach (var type in (AmmunitionType[]) Enum.GetValues(typeof(AmmunitionType)))
            {
                var data = _dataProvider.GetBulletData(type);
                await _assetProvider.Load<GameObject>(data.Template);
            }
        }

        public async Task<Player> CreatePlayer(Transform parent)
        {
            var template = await _assetProvider.Load<GameObject>(AssetAddress.Player);
            var player = _container
                .InstantiatePrefab(template, parent.position, Quaternion.identity, parent)
                .GetComponent<Player>();

            return player;
        }

        public async Task<PlayerInput> CreateInput()
        {
            if (_inputService != null) return _inputService;
            
            var template = await _assetProvider.Load<GameObject>(AssetAddress.PlayerInput);
            _inputService = _container.InstantiatePrefab(template).GetComponent<PlayerInput>();
            return _inputService;
        }

        public async Task<LoadingCurtain> CreateCurtain()
        {
            var template = await _assetProvider.Load<GameObject>(AssetAddress.LoadingCurtain);
            var curtain = _container.InstantiatePrefab(template);
            return curtain.GetComponent<LoadingCurtain>();
        }

        public async Task<GameObject> CreateEnemy(EnemyType enemyType, Transform parent)
        {
            var enemyData = _dataProvider.GetEnemyData(enemyType);
            var template = await _assetProvider.Load<GameObject>(enemyData.Template);
            var enemy = _container.InstantiatePrefab(template, parent.position, Quaternion.identity, parent);

            return enemy;
        }
        
        public async Task<GameObject> CreateBullet(AmmunitionType ammunitionType, Transform parent)
        {
            var data = _dataProvider.GetBulletData(ammunitionType);
            var template = await _assetProvider.Load<GameObject>(data.Template);
            Bullet bullet;

            if (_bulletPool.InactiveCount > 0)
            {
                bullet = _bulletPool.Get(); 
            }
            else
            {
                bullet = _container.InstantiatePrefab(template).GetComponent<Bullet>();
                bullet.Destroyed += _bulletPool.Release;
            }

            var bulletData = _dataProvider.GetBulletData(ammunitionType);
            bullet.Init(bulletData);
            bullet.transform.SetPositionAndRotation(parent.position, parent.rotation);
            
            return bullet.gameObject;
        }

        public Task<GameObject> CreateWeapon(WeaponType weaponType, Transform parent)
        {
            return weaponType switch
            {
                WeaponType.Colt => CreateColt(parent),
                WeaponType.M4 => CreateM4(parent),
                WeaponType.Sword => CreateSword(parent),
                _ => throw new ArgumentNullException(nameof(weaponType))
            };
        }

        private async Task<BulletPool> CreateBulletPool()
        {
            var template = await _assetProvider.Load<GameObject>(AssetAddress.BulletPool);
            return _container.InstantiatePrefab(template).GetComponent<BulletPool>();
        }

        private async Task<GameObject> CreateColt(Transform parent)
        {
            var weaponData = (FireArmData)_dataProvider.GetWeaponData(WeaponType.Colt);
            var template = await _assetProvider.Load<GameObject>(weaponData.Template);
            var colt = _container.InstantiatePrefab(template, parent.position, Quaternion.identity, parent);

            return colt;
        }

        private async Task<GameObject> CreateM4(Transform parent)
        {
            var weaponData = (FireArmData)_dataProvider.GetWeaponData(WeaponType.M4);
            var template = await _assetProvider.Load<GameObject>(weaponData.Template);
            var m4 = _container.InstantiatePrefab(template, parent.position, Quaternion.identity, parent);
                        
            return m4;
        }

        private async Task<GameObject> CreateSword(Transform parent)
        {
            var weaponData = (MeleeWeaponData)_dataProvider.GetWeaponData(WeaponType.Sword);
            var template = await _assetProvider.Load<GameObject>(weaponData.Template);
            var sword = _container.InstantiatePrefab(template, parent.position, Quaternion.identity, parent);

            return sword;
        }
    }
}