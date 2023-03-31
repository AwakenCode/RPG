using System;
using System.Threading.Tasks;
using UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class SceneLoader
    {
        private LoadingCurtain _loadingCurtain;
        
        public SceneLoader(Task<LoadingCurtain> loadingCurtain)
        {
            InitCurtain(loadingCurtain);
        }
        
        public async void Load(string name, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == name)
            {
                onLoaded?.Invoke();
                return;
            }

            _loadingCurtain.Show();
            
            await Addressables.LoadSceneAsync(name).Task; 
            
            onLoaded?.Invoke();
            _loadingCurtain.Hide();
        }

        private async void InitCurtain(Task<LoadingCurtain> loadingCurtain)
        {
            await loadingCurtain;
            _loadingCurtain = loadingCurtain.Result;
        }
    }
}