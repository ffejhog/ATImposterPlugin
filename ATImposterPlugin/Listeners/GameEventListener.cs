using System;
using System.Collections.Generic;
using System.Text;
using Impostor.Api.Events;
using Impostor.Api.Events.Player;
using Microsoft.Extensions.Logging;

namespace ATImposterPlugin.Listeners
{
    public class GameEventListener : IEventListener
    {
        private readonly ILogger<ATImposterPlugin> _logger;

        public GameEventListener(ILogger<ATImposterPlugin> logger)
        {
            _logger = logger;
        }

        [EventListener(EventPriority.Monitor)]
        public void OnGame(IGameEvent e)
        {
            _logger.LogInformation(e.GetType().Name + " triggered");
        }

        [EventListener]
        public void OnGameCreated(IGameCreatedEvent e)
        {
            _logger.LogInformation("Game > created");
        }

        [EventListener]
        public void OnGameStarting(IGameStartingEvent e)
        {
            _logger.LogInformation("Game > starting");
        }

        [EventListener]
        public void OnGameStarted(IGameStartedEvent e)
        {
            _logger.LogInformation("Game > started");

            foreach (var player in e.Game.Players)
            {
                var info = player.Character.PlayerInfo;

                _logger.LogInformation($"- {info.PlayerName} {info.IsImpostor}");
            }
        }

        [EventListener]
        public void OnGameEnded(IGameEndedEvent e)
        {
            _logger.LogInformation("Game > ended");
        }

        [EventListener]
        public void OnGameDestroyed(IGameDestroyedEvent e)
        {
            _logger.LogInformation("Game > destroyed");
        }

        [EventListener]
        public void OnPlayerJoined(IGamePlayerJoinedEvent e)
        {
            _logger.LogInformation("Player joined a game.");
        }

        [EventListener]
        public void OnPlayerLeftGame(IGamePlayerLeftEvent e)
        {
            _logger.LogInformation("Player left a game.");
        }
    }
}
