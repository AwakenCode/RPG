using Character;
using Data.Enemy;
using Service;
using Service.Data;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyType _enemyType;

        private TaskProvider _taskProvider;

        [Inject]
        private void Construct(IDataProvider dataProvider, TaskProvider taskProvider)
        {
            _taskProvider = taskProvider;
            DataProvider = dataProvider;
            Data = DataProvider.GetEnemyData(_enemyType);
        }
        
        public EnemyData Data { get; private set; }
        public Player Target { get; private set; }
        public IDataProvider DataProvider { get; private set; }

        private void Awake()
        {
            Init();
        }

        private async void Init() => Target = await _taskProvider.GetTask<Player>();
    }
}

