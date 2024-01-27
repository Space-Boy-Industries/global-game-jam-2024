using UnityEngine;

public class CleanTableTest : MonoBehaviour
{
    private void OnMouseDown()
    {
        GlobalStateSystem.Instance.SetFlag("IsTableClean", true);
    }
}
