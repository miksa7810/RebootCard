using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public List<CharacterCard> playerAreaCharacters;

    public List<CharacterCard> GetAllCharacters()
    {
        return playerAreaCharacters;
    }

    public CharacterCard GetOpponentCharacterInSameLane(MissionCard missionCard)
    {
        // 仮で null を返す。あとでちゃんと実装。
        return null;
    }
}
