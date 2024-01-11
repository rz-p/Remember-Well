using UnityEngine;
using UnityEngine.EventSystems; // For handling click events

public class CardManager : MonoBehaviour, IPointerClickHandler
{
    public SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component

    public Sprite frontSprite; // Assign in inspector or via script
    public Sprite backSprite; // Assign in inspector

    private bool isFlipped = false;

    // Identifier for the card, useful for matching logic
    public int cardId { get; private set; }

    public void SetCard(Sprite front, int id)
    {
        frontSprite = front;
        cardId = id;
    }

    public void FlipCard()
    {
        isFlipped = !isFlipped;

        // Change the sprite based on whether the card is flipped
        spriteRenderer.sprite = isFlipped ? frontSprite : backSprite;
        
        // Additional logic for when the card is flipped
    }

    void Start()
    {
        spriteRenderer.sprite = backSprite; // Start with the back sprite
    }

     public void SetFrontImage(Sprite sprite)
    {
        frontSprite = sprite; // Assign the sprite for the front image
    }

    // Handle click events on the card
    public void OnPointerClick(PointerEventData eventData)
    {
        FlipCard();

        // Notify GameManager or another script that this card was clicked
    }
}
