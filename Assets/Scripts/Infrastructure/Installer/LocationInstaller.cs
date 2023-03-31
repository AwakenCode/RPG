using Zenject;
using UnityEngine;
using Infrastructure.Factory;
using Marker;
using Service;

namespace Infrastructure.Installer
{
    public class LocationInstaller : MonoInstaller
    {
        [SerializeField] private Transform _startPoint;

        public override void Start()
        {
            base.Start();
            InitWorld();
        }

        public override void InstallBindings()
        {
            BindPlayer();
        }
        
        private void BindPlayer()
        {
            var player = Container
                .Resolve<IGameFactory>()
                .CreatePlayer(_startPoint);

            Container
                .Resolve<TaskProvider>()
                .AddTask(player);
        }
        
        private void InitWorld()
        {
            var gameFactory = Container.Resolve<IGameFactory>();

            gameFactory.Initialize();

            InitEnemies();
            InitWeapons();
        }

        private void InitEnemies()
        {
            var enemyMarkers = FindObjectsOfType<EnemyMarker>();
            var gameFactory = Container.Resolve<IGameFactory>();

            foreach (var enemyMarker in enemyMarkers)
                gameFactory.CreateEnemy(enemyMarker.EnemyType, enemyMarker.transform);
        }

        private void InitWeapons()
        {
            var weaponMarkers = FindObjectsOfType<WeaponMarker>();
            var gameFactory = Container.Resolve<IGameFactory>();

            foreach (var weaponMarker in weaponMarkers)
                gameFactory.CreateWeapon(weaponMarker.WeaponType, weaponMarker.transform);
        }
    }
}