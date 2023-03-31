using System.Collections.Generic;
using System.Linq;
using Data.Enemy;
using Data.Player;
using Data.Weapon;
using Enemy;
using UnityEngine;
using Weapon;
using Weapon.Ammunition;

namespace Service.Data
{
    public class DataProvider : IDataProvider
    {
        private Dictionary<EnemyType, EnemyData> _enemyData;
        private Dictionary<WeaponType, WeaponData> _weaponData;
        private Dictionary<WeaponType, WeaponTransformData> _weaponTransformData;
        private Dictionary<AmmunitionType, BulletData> _bulletData;
        private PlayerData _playerData;

        public void Load()
        {
            _enemyData = Resources
                .LoadAll<EnemyData>(DataPath.EnemyData)
                .ToDictionary(data => data.EnemyType, data => data);

            _playerData = Resources.Load<PlayerData>(DataPath.PlayerData);

            _weaponTransformData = Resources
                .LoadAll<WeaponTransformData>(DataPath.WeaponTransformData)
                .ToDictionary(data => data.Type, data => data);

            _weaponData = Resources
                .LoadAll<WeaponData>(DataPath.WeaponData)
                .ToDictionary(data => data.Type, data => data);

            _bulletData = Resources
                .LoadAll<BulletData>(DataPath.BulletData)
                .ToDictionary(data => data.Type, data => data);
        }

        public PlayerData GetPlayerData() => _playerData;

        public EnemyData GetEnemyData(EnemyType type) =>
            _enemyData.TryGetValue(type, out var enemyData) ? enemyData : null;

        public WeaponData GetWeaponData(WeaponType type) =>
            _weaponData.TryGetValue(type, out var weaponData) ? weaponData : null;

        public BulletData GetBulletData(AmmunitionType type) => 
            _bulletData.TryGetValue(type, out var bulletData) ? bulletData : null;

        public WeaponTransformData GetWeaponTransformData(WeaponType type) =>
            _weaponTransformData.TryGetValue(type, out var weaponTransformData) ? weaponTransformData : null;
    }
}