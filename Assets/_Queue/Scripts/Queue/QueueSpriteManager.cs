using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueSpriteManager : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> _sprites;
    
    //Sorting layers
    private const int _startSortingOrder = 1;

    public void UpdateSprites(List<IConsumer> consumers)
    {
        _sprites = new List<SpriteRenderer>();

        for (int i = 0; i < consumers.Count; i++)
        {
            _sprites.Add(((Consumer)consumers[i]).SpriteRenderer);
            //_sprites[i].sortingOrder = _startSortingOrder + consumers.Count - i;
        }
    }
}
