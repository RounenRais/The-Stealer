using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;
[CreateAssetMenu(fileName = "NewEnemy", menuName = "Battle/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public int maxHP;
    public Sprite battleSprite;    
    public Sprite worldSprite;     
    public List<AttackData> attackList;
    public List<Sprite> animationSprites;

    public int expReward;          
}