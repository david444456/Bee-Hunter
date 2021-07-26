using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeHunter.Core
{
    public abstract class Item : ScriptableObject
    {
        [SerializeField] protected int _actualIndex;
        [SerializeField] protected Sprite _spriteItem;

        public abstract string GetNameItem();

        public Sprite GetActualSpriteByItem() => _spriteItem;

        public int GetActualIndexItem() => _actualIndex;
    }
}
