using System;
using System.Threading.Tasks;
using Impostor.Api.Events.Managers;
using Impostor.Api.Plugins;
using Microsoft.Extensions.Logging;
using ATImposterPlugin.Listeners;

namespace ATImposterPlugin
{
    /// <summary>
    ///     The metadata information of your plugin, this is required.
    /// </summary>
    [ImpostorPlugin(
        package: "com.jeffreyneer.ATImposterPlugin",
        name: "ATImposterPlugin Plugin",
        author: "Jeffrey Neer",
        version: "1.0.0")]
    public class ATImposterPlugin : PluginBase
    {
        /// <summary>
        ///     A logger that works seamlessly with the server.
        /// </summary>
        private readonly ILogger<ATImposterPlugin> _logger;

        private readonly IEventManager _eventManager;

        private IDisposable[] _cancel;


        /// <summary>
        ///     The constructor of the plugin. There are a few parameters you can add here and they
        ///     will be injected automatically by the server, two examples are used here.
        ///
        ///     They are not necessary but very recommended.
        /// </summary>
        /// <param name="logger">
        ///     A logger to write messages in the console.
        /// </param>
        /// <param name="eventManager">
        ///     An event manager to register event listeners.
        ///     Useful if you want your plugin to interact with the game.
        /// </param>
        public ATImposterPlugin(ILogger<ATImposterPlugin> logger, IEventManager eventManager)
        {
            _logger = logger;
            _eventManager = eventManager;
        }

        /// <summary>
        ///     This is called when your plugin is enabled by the server.
        /// </summary>
        /// <returns></returns>
        public override ValueTask EnableAsync()
        {
            _logger.LogInformation("ATImposterPlugin is being enabled.");
            _cancel = new[]
            {
                _eventManager.RegisterListener(new GameEventListener(_logger)),
                _eventManager.RegisterListener(new MeetingEventListener(_logger)),
                _eventManager.RegisterListener(new PlayerEventListener(_logger))
            };
            return default;
        }

        /// <summary>
        ///     This is called when your plugin is disabled by the server.
        ///     Most likely because it is shutting down, this is the place to clean up any managed resources.
        /// </summary>
        /// <returns></returns>
        public override ValueTask DisableAsync()
        {
            _logger.LogInformation("ATImposterPlugin is being disabled.");
            foreach (var c in _cancel)
            {
                c.Dispose();
            }
            return default;
        }
    }
}
