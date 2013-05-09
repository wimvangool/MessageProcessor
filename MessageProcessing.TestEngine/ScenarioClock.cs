﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace YellowFlare.MessageProcessing
{
    /// <summary>
    /// Represents a clock that is used by each <see cref="Scenario{T}" /> to control the timeline in which the
    /// scenario is executed.
    /// </summary>
    internal sealed class ScenarioClock : IClock
    {
        private readonly List<DateTime> _requests;
        private readonly DateTime _clockOffset;
        private Stopwatch _stopwatch;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScenarioClock" /> class.
        /// </summary>
        /// <param name="clockOffset">The date and time to which the clock is initialized.</param>
        public ScenarioClock(DateTime clockOffset)
        {
            _requests = new List<DateTime>();
            _clockOffset = clockOffset;
        }

        /// <summary>
        /// Starts the clock.
        /// </summary>
        public void Start()
        {
            _stopwatch = Stopwatch.StartNew();
        }

        /// <summary>
        /// Stops the lock.
        /// </summary>
        public void Stop()
        {
            _stopwatch.Stop();
        }
        
        public DateTime? RequestAt(int index)
        {
            if (index < 0)
            {
                throw NewIndexOutOfRangeException("index");
            }
            if (index < _requests.Count)
            {
                return _requests[index];
            }
            return null;
        }        

        /// <inheritdoc />
        public DateTime CurrentDate()
        {
            return RequestDateTime().Date;
        }

        /// <inheritdoc />
        public DateTime CurrentDateTime()
        {
            return RequestDateTime();
        }

        /// <inheritdoc />
        public DateTime CurrentDateUtc()
        {
            return RequestDateTime().ToUniversalTime().Date;
        }

        /// <inheritdoc />
        public DateTime CurrentDateTimeUtc()
        {
            return RequestDateTime().ToUniversalTime();
        }        

        private DateTime RequestDateTime()
        {
            var dateTime = _clockOffset.AddMilliseconds(_stopwatch.ElapsedMilliseconds);
            _requests.Add(dateTime);
            return dateTime;
        }

        private static Exception NewIndexOutOfRangeException(string index)
        {
            var messageFormat = ExceptionMessages.Scenario_IndexNegative;
            var message = string.Format(CultureInfo.CurrentCulture, messageFormat, index);
            return new ArgumentOutOfRangeException("index", message);
        }
    }
}
