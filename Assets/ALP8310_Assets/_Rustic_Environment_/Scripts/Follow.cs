using UnityEngine;

[ExecuteInEditMode]
public class Follow : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        if(player == null)
        {
            return;
        }

        transform.position = player.transform.position;
    }
}
