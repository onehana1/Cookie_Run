using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageButton : MonoBehaviour
{
    public void InfiniteMode()
    {
        StageManager.Instance.ModeSelete(Mode.Infinite);
    }

    public void StoryMode()
    {
        StageManager.Instance.ModeSelete(Mode.Story);
    }
}
