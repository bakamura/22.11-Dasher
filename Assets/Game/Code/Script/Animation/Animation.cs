using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Animation")]
public class Animation : ScriptableObject {

    public Sprite[] sprites;
    public float speed = 1;
    public bool loop = true;

}
