using System.Threading.Tasks;
using Service.Asset;
using UnityEngine;

namespace UI.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;
        
        public UIFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        
        public async Task CreateLoadingCurtain()
        {
            await _assetProvider.Instantiate(AssetAddress.LoadingCurtain);
        }
    }

    public interface IUIFactory
    {
        Task CreateLoadingCurtain();
    }
}