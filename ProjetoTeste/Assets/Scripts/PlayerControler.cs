using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerControler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] string playerName;
    [SerializeField] Sprite sprite;
    [SerializeField] float moveSpeed = 5f;
    private Vector2 input;

    private Character character;

    public string PlayerName { get => playerName; }
    public Sprite Sprite { get => sprite; }
        
    public Character Character { get => character; }

    private void Awake()
    {
        character = GetComponent<Character>();
    }
    void Start()
    {
    }

    // Update is called once per frame
    public void HandleUpdate()
    {
        if (!character.IsMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            input.Normalize();

            MovePlayer();
        }

        character.HandleUpdate();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Interact();
        }
    }

    private void MovePlayer()
    {
        // Calculate the movement amount based on input and speed
        Vector3 movement = new Vector3(input.x, input.y, 0) * moveSpeed * Time.deltaTime;

        // Apply the movement
        transform.Translate(movement);
    }

    void Interact()
    {
        var faceDirection = new Vector3(character.Animator.MoveX, character.Animator.MoveY);
        var interactPos = transform.position + faceDirection;

        var collider = Physics2D.OverlapCircle(interactPos, 0.3f, GameLayers.i.InteractableLayer);

        if (collider != null)
        {
            Debug.Log(collider.GetComponent<Interactable>());
            collider.GetComponent<Interactable>()?.Interact(transform);
        }
    }

    private void OnMoveOver()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, 0.2f, GameLayers.i.TriggerableLayer);

        foreach (var collider in colliders)
        {
            var triggerable = collider.GetComponent<IPlayerTriggerable>();
            if (triggerable != null)
            {
                character.Animator.IsMoving = false;
                triggerable.OnPlayerTriggered(this);
                break;
            }
        }
    }
}
