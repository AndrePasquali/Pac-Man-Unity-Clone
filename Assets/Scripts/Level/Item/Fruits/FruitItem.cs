using Aquiris.PacMan.Level.Item.Fruits;
using UnityEngine;

[CreateAssetMenu(fileName = "Fruit", menuName = "Create Fruit", order = 2)]
public class FruitItem : ScriptableObject
{
    public Fruit Fruit;

    public int Points;

    public Sprite Sprite;
}
