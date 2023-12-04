using System;
using System.Collections.Generic;
using System.Linq;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace GameServices
{
    public class Economy : SingletonBehaviour<Economy>
    {
        private const string LogClassName = "Economy";
        private List<(Currency, Action<int>)> listenerMap = new();
        private Dictionary<string, int> economyValues = new();
        private string key = "Economy_";

        public enum Currency
        {
            Coin = 0,
            Gem = 1,
        }

        public void WatchCurrency(Currency currency, Action<int> handler)
        {
            listenerMap.Add((currency, handler));
        }

        public void UnWatchCurrency(Currency currency, Action<int> handler)
        {
            listenerMap.Remove((currency, handler));
        }

        private void CallListeners(Currency currency)
        {
            int value = GetCurrency(currency);
            foreach (var tuple in listenerMap.Where(x => x.Item1 == currency))
            {
                tuple.Item2?.Invoke(value);
            }
        }

        public void AddCurrency(Currency currency, int valueDelta)
        {
            int value = GetCurrency(currency) + valueDelta;
            _SetCurrency(currency, value);

            DebugX.Log($"{LogClassName} : Add Currency {currency.ToString()} : {valueDelta}", LogFilters.Economy, gameObject);
        }

        public void SubtractCurrency(Currency currency, int valueDelta)
        {
            int value = GetCurrency(currency) - valueDelta;
            _SetCurrency(currency, value);

            DebugX.Log($"{LogClassName} : Subtract Currency {currency.ToString()} : {valueDelta}", LogFilters.Economy, gameObject);
        }

        public void SetCurrency(Currency currency, int valueAbsolute)
        {
            _SetCurrency(currency, valueAbsolute);

            DebugX.Log($"{LogClassName} : Set Currency {currency.ToString()} : {valueAbsolute}", LogFilters.Economy, gameObject);
        }

        private void _SetCurrency(Currency currency, int valueAbsolute)
        {
            economyValues[key + currency] = valueAbsolute;
            var updatedValues = new Hashtable() { [key + currency] = valueAbsolute, };
            PUNRoomHandler.SetLocalPlayerProperties(updatedValues);
            CallListeners(currency);
        }

        public int GetCurrency(Currency currency)
        {
            return economyValues.TryGetValue(key + currency, out var value) ? value : 0;
        }

        public void Reset()
        {
            economyValues.Clear();
        }
    }
}