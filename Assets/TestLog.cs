using UnityEngine;

using Cathode.Logger;

public class TestLog : MonoBehaviour
{
    private void Awake()
    {
        LoggingFilter.Log("Environment", "this is a message from environment");
        LoggingFilter.Log("Player", "this is a message from player");
        LoggingFilter.Log("Player/Movement", "this is a message from player movement");
        LoggingFilter.Log("Player/Movement", "this is a second message from player movement");
    }

    private void OnDrawGizmosSelected()
    {
        LoggingFilter.Log("Player/Movement", "this is a third message from player movement inside giwm");
    }
}
