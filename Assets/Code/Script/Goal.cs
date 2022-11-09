using UnityEngine;

public class Goal : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) Debug.Log("Show Success Display");
    }

}
