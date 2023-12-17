﻿using System;
using System.Diagnostics.CodeAnalysis;
using Cysharp.Threading.Tasks;
using Extreal.Core.Common.Retry;
using Extreal.Core.Common.System;
using Extreal.Core.Logging;
using Extreal.Core.StageNavigation;
using Extreal.Integration.AssetWorkflow.Addressables;
using Extreal.Integration.P2P.WebRTC;
using Extreal.Integration.Multiplay.Common.MVS.App.Config;
using UniRx;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Extreal.Integration.Multiplay.Common.MVS.App.AssetWorkflow
{
    public class AssetHelper : DisposableBase
    {
        public IObservable<string> OnDownloading => assetProvider.OnDownloading;
        public IObservable<AssetDownloadStatus> OnDownloaded => assetProvider.OnDownloaded;
        public IObservable<int> OnConnectRetrying => assetProvider.OnConnectRetrying;
        public IObservable<bool> OnConnectRetried => assetProvider.OnConnectRetried;

        public PeerConfig PeerConfig { get; private set; }
        public HostConfig NgoHostConfig { get; private set; }
        public ClientConfig NgoClientConfig { get; private set; }

        private static readonly ELogger Logger = LoggingManager.GetLogger(nameof(AssetHelper));

        private readonly StageNavigator<StageName, SceneName> stageNavigator;
        private readonly AssetProvider assetProvider;
        private readonly AppState appState;

        [SuppressMessage("Usage", "CC0033")]
        private readonly CompositeDisposable assetDisposables = new CompositeDisposable();

        [SuppressMessage("Usage", "CC0022")]
        public AssetHelper(
            AppConfig appConfig, StageNavigator<StageName, SceneName> stageNavigator, AppState appState)
        {
            this.stageNavigator = stageNavigator;
            this.appState = appState;
            assetProvider = new AssetProvider(new CountingRetryStrategy(appConfig.DownloadMaxRetryCount));
        }

        protected override void ReleaseManagedResources()
        {
            assetDisposables.Dispose();
            assetProvider.Dispose();
        }

        public UniTask<AssetDisposable<T>> LoadAssetAsync<T>(string assetName)
            => assetProvider.LoadAssetAsync<T>(assetName);

        public UniTask<AssetDisposable<SceneInstance>> LoadSceneAsync(string assetName)
            => assetProvider.LoadSceneAsync(assetName);
    }
}
