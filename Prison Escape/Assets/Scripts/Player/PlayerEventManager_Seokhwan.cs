public partial class PlayerEventManager
{
    private void AddTrapManager()
    {
        if (GetComponent<PlayerTrapManager>() == null)
        {
            gameObject.AddComponent<PlayerTrapManager>();
        }
    }

    private void AddSaveManager()
    {
        if (GetComponent<PlayerSaveManager>() == null)
        {
            gameObject.AddComponent<PlayerSaveManager>();
        }
    }
}