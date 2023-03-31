using Enemy;
using System.Threading.Tasks;
using Character;
using Service.Input;
using UI;
using UnityEngine;
using Weapon;
using Weapon.Ammunition;

namespace Infrastructure.Factory
{
    public interface IGameFactory
    {
        void Initialize();
        Task<PlayerInput> CreateInput();
        Task<Player> CreatePlayer(Transform parent);
        Task<LoadingCurtain> CreateCurtain();
        Task<GameObject> CreateEnemy(EnemyType enemyType, Transform parent);
        Task<GameObject> CreateBullet(AmmunitionType ammunitionType, Transform parent);
        Task<GameObject> CreateWeapon(WeaponType weaponType, Transform parent);
    }
}
