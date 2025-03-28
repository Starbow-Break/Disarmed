public partial class PlayerEventManager
{
    private void AddSaveManager()
    {
        if (GetComponent<PlayerSaveManager>() == null)
        {
            gameObject.AddComponent<PlayerSaveManager>();
        }
    }
}