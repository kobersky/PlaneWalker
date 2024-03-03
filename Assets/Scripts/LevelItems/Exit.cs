using UnityEngine;

/* represents the Exit of a level */
public class Exit : MonoBehaviour, IInteractable, IUnlockable
{
    [SerializeField] GameObject _particleEffectPrefab;
    [SerializeField] Animator _animator;

    private float MAX_PARTICLE_LIFETIME = 2f; 
    private ItemType _itemType;

    public bool IsOpen { get; private set; }

    private void Awake()
    {
        _itemType = ItemType.ExitItem;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagNames.PLAYER))
        {
            OnInteract();
        }
    }

    public void OnInteract()
    {
        GameObject particleEffect = Instantiate(_particleEffectPrefab, transform.position, Quaternion.identity);
        Destroy(particleEffect, MAX_PARTICLE_LIFETIME);
    }

    public ItemType GetItemType()
    {
        return _itemType;
    }

    public void OnUnlocked()
    {
        _animator.SetTrigger(AnimationKeys.OPEN_GATE);
    }
}
