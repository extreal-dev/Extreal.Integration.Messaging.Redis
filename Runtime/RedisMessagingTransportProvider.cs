using System;

namespace Extreal.Integration.Messaging.Redis
{
    /// <summary>
    /// Class that provides RedisMessagingTransport.
    /// </summary>
    public static class RedisMessagingTransportProvider
    {
        /// <summary>
        /// Provides RedisMessagingTransport.
        /// </summary>
        /// <remarks>
        /// Creates and returns a RedisMessagingTransport for Native (C#) or WebGL (JavaScript) depending on the platform.
        /// </remarks>
        /// <param name="messagingConfig">Messaging config for Redis.</param>
        /// <exception cref="ArgumentNullException">When messagingConfig is null.</exception>
        /// <returns>RedisMessagingTransport.</returns>
        public static RedisMessagingTransport Provide(RedisMessagingConfig messagingConfig)
        {
            if (messagingConfig == null)
            {
                throw new ArgumentNullException(nameof(messagingConfig));
            }

#if !UNITY_WEBGL || UNITY_EDITOR
            return new NativeRedisMessagingTransport(messagingConfig);
#else
            return new WebGLRedisMessagingTransport(new WebGLRedisMessagingConfig(messagingConfig));
#endif
        }
    }
}
