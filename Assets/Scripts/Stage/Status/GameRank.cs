using System.Linq;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

namespace Rabbit
{
    public class GameRank : MonoBehaviour
    {
        [System.Serializable]
        public struct RankInfo
        {
            [Tooltip("Condition 이상일 경우 이 랭크를 획득")]
            public float condition;
            [Multiline]
            public string desc;
        }
        
        [System.Serializable]
        public class RankDictionary : SerializableDictionaryBase<string, RankInfo> { }

        [SerializeField] private RankDictionary rankDic;
        
        public (string rank, string desc) GetRank(float score)
        {
            var allPerfectScore = StageManager.LevelData.count * (StageManager.LevelData.count + 1) / 2;
            var rank = score / (allPerfectScore * StageManager.ScorePreset.defaultNoteScore);

            foreach (var rankInfo in rankDic.Where(rankInfo => rank >= rankInfo.Value.condition)) {
                print($"My Rank : {rank}, Check Rank : ~ > {rankInfo.Value.condition} / {rankInfo.Key}");
                return (rankInfo.Key, rankInfo.Value.desc);
            }

            return ("N", "ERROR VALUE");
        }
    }
}