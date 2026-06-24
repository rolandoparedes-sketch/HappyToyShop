using UnityEngine;

public class Cesta : MonoBehaviour, IInteractuable
{
    public void Interact()
    {
        var player = PlayerController2D.instance.playerMechanics;

        if (!player.HasGift||!player.ToyData)
        {

            GameManager2D.instance.UIManager.ChangeDialoguePlayer("I can only get rid of toys that are already wrapped");
            return;
        }
        player.HasGift = false;
        player.Gift.gameObject.SetActive(false);

        player.RemoveToy();
        ShelfStorage.OnTakeToy?.Invoke();

        GameManager2D.instance.SoundManager.CheckTypeAudio(SoundType.SFX, 4);

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
