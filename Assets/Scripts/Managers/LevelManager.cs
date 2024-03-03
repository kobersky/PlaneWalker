using UnityEngine;

/* manages all level objects  */

public class LevelManager : MonoBehaviour
{
    public Transform BaseSurface;
    public Transform AdditionalPlatforms;
    public Transform SpecialObjects;

    public GameObject PlayerStartingPoint;
    public GameObject Exit;
    public GameObject VitalItem;

    public void ResetItems()
    {
        for (int i = AdditionalPlatforms.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(AdditionalPlatforms.GetChild(i).gameObject);
        }

        for (int i = SpecialObjects.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(SpecialObjects.GetChild(i).gameObject);
        }
    }

    public void UnlockExit()
    {
        if (Exit.TryGetComponent<IUnlockable>(out IUnlockable unlockable))
        {
            unlockable.OnUnlocked();
        }
    }
}
