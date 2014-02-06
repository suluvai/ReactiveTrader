﻿using System.Collections.Generic;
using System.Linq;
using Adaptive.ReactiveTrader.Contracts;
using log4net;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Adaptive.ReactiveTrader.Server.ReferenceData
{
    [HubName(ServiceConstants.Server.ReferenceDataHub)]

    public class ReferenceDataHub : Hub
    {
        private readonly ICurrencyPairRepository _currencyPairRepository;
        private static readonly ILog Log = LogManager.GetLogger(typeof(ReferenceDataHub));

        public ReferenceDataHub(ICurrencyPairRepository currencyPairRepository)
        {
            _currencyPairRepository = currencyPairRepository;
        }

        [HubMethodName(ServiceConstants.Server.GetCurrencyPairs)]
        public IEnumerable<CurrencyPair> GetCurrencyPairs()
        {
            Log.InfoFormat("Received request for currency pairs from connection {0}", Context.ConnectionId);

            var currencyPairs = _currencyPairRepository.GetAllCurrencyPairs().ToList();
            Log.InfoFormat("Sending {0} currency pairs to {1}'", currencyPairs.Count, Context.ConnectionId);

            return currencyPairs;
        }
    }
}