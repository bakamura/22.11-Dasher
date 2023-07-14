using UnityEngine;

public class Goal : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) StartCoroutine(
            Hud
            .Instance.
            ShowSuccessDisplay());
    }

}
