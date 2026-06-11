using System.Collections.Generic;
using UnityEngine;

namespace Code.Game
{
    [CreateAssetMenu(fileName = "Character", menuName = "Character", order = 0)]
    public class CharacterData : ScriptableObject
    {
        public string Name;
        public string UFXIcon;
        public Sprite Icon;

        public string[] Messages;
        public string TriggerMessage;
        public Dictionary<TriggerType, string> Triggers;
    }
}