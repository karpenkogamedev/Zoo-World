using System.Collections.Generic;
using UnityEngine;

namespace ZooWorld.Configs.UI
{
    [CreateAssetMenu(fileName = "UIDisplayConfig", menuName = "ZooWorld/Configs/UI/UI Display Config")]
    public class UIDisplayConfig : ScriptableObject
    {
        [SerializeField] private List<StatisticsHudEntryConfig> _statisticsEntries = new();

        public IReadOnlyList<StatisticsHudEntryConfig> StatisticsEntries => _statisticsEntries;
    }
}
