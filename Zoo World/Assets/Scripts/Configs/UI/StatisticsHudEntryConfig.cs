using System;
using UnityEngine;
using UnityEngine.Serialization;
using ZooWorld.Gameplay.Statistics;

namespace ZooWorld.Configs.UI
{
    [Serializable]
    public class StatisticsHudEntryConfig
    {
        private static readonly string DefaultCounterFormat = "{0}: {1}";

        [FormerlySerializedAs("_animalRole")]
        [SerializeField] private AnimalStatisticsRole _statisticsRole;
        [SerializeField] private string _label;
        [SerializeField] private string _counterFormat = DefaultCounterFormat;
        public AnimalStatisticsRole StatisticsRole => _statisticsRole;

        public string Label
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_label) == false)
                    return _label;
                
                return _statisticsRole.ToString();
            }
        }

        public string CounterFormat => string.IsNullOrWhiteSpace(_counterFormat)
            ? DefaultCounterFormat
            : _counterFormat;
    }
}
