﻿using UnityEngine;

namespace Extreal.Integration.Messaging.Redis.MVS.App.Avatars
{
    public class AvatarProvider : MonoBehaviour
    {
        [SerializeField] private Avatar avatar;

        public Avatar Avatar => avatar;
    }
}
