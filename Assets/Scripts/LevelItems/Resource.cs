using UnityEngine;

/* represents a resource that can be collected by the player */
public class Resource : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject _particleEffectPrefab;

    private ItemType _itemType;
    private float MAX_PARTICLE_LIFETIME = 2f;

    private void Awake()
    {
        _itemType = ItemType.ResourceItem;
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
        Destroy(gameObject);
    }

    public ItemType GetItemType()
    {
        return _itemType;
    }
}
