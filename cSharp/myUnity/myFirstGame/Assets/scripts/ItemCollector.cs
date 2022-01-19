using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // import TextMeshPro

public class ItemCollector : MonoBehaviour
{
    int coins = 0; 
    [SerializeField] string TAG_COIN = "coin";
    [SerializeField] TextMeshProUGUI COIN_TEXTMESH;
    [SerializeField] AudioSource collectionSound;

    // Use OnTirggerEnter method instead of OnCollisionEnter when Is Trigger checked.
    // OnTirgger parameter type should be Collider.
    private void OnTriggerEnter(Collider coin)
    {
        // GameObject.CompareTag : Is this game object tagged with tag ?
        if (coin.gameObject.CompareTag(TAG_COIN)) {

            coins++;
            // Component : Base class for everything attached to GameObjects.
            // Componet.gameObject : The game object this component is attached to. 
            // A component is always attached to a game object.
            Destroy(coin.gameObject);
            collectionSound.Play();
            COIN_TEXTMESH.text = "Coins : " + coins;
        }
    }
}
