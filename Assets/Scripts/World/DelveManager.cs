
using UnityEngine;

public class DelveManager : MonoBehaviour
{
    private DelveRoomGenerator _roomGenerator;


    private void Awake()
    {
        _roomGenerator = GetComponent<DelveRoomGenerator>();
    }

    private void Start()
    {
        _roomGenerator.Generate(128, 128,4,0.04285f);
    }
}
