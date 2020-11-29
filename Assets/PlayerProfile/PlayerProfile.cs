using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Player Profile")]
public class PlayerProfile : ScriptableObject
{
    public int activeDeckIndex;
    public List<Deck> decks;
    public List<string> deckFolders;

    [Button]
    public void GenerateDecksFromPaths()
    {
        foreach (string folder in deckFolders)
        {
            string path = "AnkiDecks/" + folder + "/deck";
            string json = Resources.Load<TextAsset>(path).text;
            Deck deck = AnkiParser.GetDeckFromJson(json);
            deck.deckMediaPath = "AnkiDecks/" + folder + "/media";
            decks.Add(deck);
        }
    }
}
