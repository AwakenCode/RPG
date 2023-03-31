using Data.Enemy;
using Data.Player;
using Data.Weapon;
using Enemy;
using Weapon;
using Weapon.Ammunition;

namespace Service.Data
{
    public interface IDataProvider
    {
        void Load();
        PlayerData GetPlayerData();
        EnemyData GetEnemyData(EnemyType type);
        WeaponData GetWeaponData(WeaponType type);
        BulletData GetBulletData(AmmunitionType type);
        WeaponTransformData GetWeaponTransformData(WeaponType type);
    }
}