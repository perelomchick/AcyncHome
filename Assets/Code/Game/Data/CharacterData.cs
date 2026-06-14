using UnityEngine;

namespace Code.Game.Data
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "CharacterData", order = 0)]
    public class CharacterData : ScriptableObject
    {
        public string Name;
        public string UFXIcon;
        public Sprite Icon;

        public string[] Messages;
        public string TriggerMessage;
    }
}