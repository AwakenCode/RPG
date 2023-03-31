using Infrastructure;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        private const string Level1 = "Level 1";
        private const string Level2 = "Level 2";
        
        [SerializeField] private Button _level1;
        [SerializeField] private Button _level2;

        private SceneLoader _sceneLoader;

        [Inject]
        private void Construct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
        private void Awake()
        {
            _level1.onClick.AddListener(OnLevel1Clicked);
            _level2.onClick.AddListener(OnLevel2Clicked);
        }

        private void OnLevel2Clicked()
        {
            _sceneLoader.Load(Level2);
        }

        private void OnLevel1Clicked()
        {
            _sceneLoader.Load(Level1);
        }
    }
}