using UnityEngine;

[CreateAssetMenu(fileName = "States Catalog", menuName = "ScriptableObjects/Create States Catalog Entry", order = 1)]
public class StateCatalogEntry : ScriptableObject
{
    [SerializeField]
    private string id;

    [SerializeField]
    private UiView uiView;

    [SerializeField]
    private WorldView worldView;

    public string Id => id;

    public UiView UiView => uiView;

    public WorldView WorldView => worldView;
}
