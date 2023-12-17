﻿using Cysharp.Threading.Tasks;
using Extreal.Core.StageNavigation;
using Extreal.Integration.Messaging.Redis.MVS.App.Config;
using VContainer.Unity;

namespace Extreal.Integration.Messaging.Redis.MVS.App
{
    public class AppPresenter : IStartable
    {
        private readonly StageNavigator<StageName, SceneName> stageNavigator;

        public AppPresenter(StageNavigator<StageName, SceneName> stageNavigator)
        {
            this.stageNavigator = stageNavigator;
        }

        public void Start()
        {
            stageNavigator.ReplaceAsync(StageName.GroupSelectionStage).Forget();

        }
    }
}