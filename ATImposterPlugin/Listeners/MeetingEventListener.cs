using System;
using System.Collections.Generic;
using System.Text;
using Impostor.Api.Events;
using Impostor.Api.Events.Meeting;
using Microsoft.Extensions.Logging;

namespace ATImposterPlugin.Listeners
{
    public class MeetingEventListener : IEventListener
    {

        private readonly ILogger<ATImposterPlugin> _logger;

        public MeetingEventListener(ILogger<ATImposterPlugin> logger)
        {
            _logger = logger;
        }

        [EventListener]
        public void OnMeetingStarted(IMeetingStartedEvent e)
        {
            _logger.LogInformation("Meeting > started");
        }

        [EventListener]
        public void OnMeetingEnded(IMeetingEndedEvent e)
        {
            _logger.LogInformation("Meeting > ended");
        }
    }
}
