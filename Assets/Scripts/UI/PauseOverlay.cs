using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
    using PlayerManager = Game.PlayerManager;

    public class PauseOverlay : Menu
    {
        public PauseOverlayController pauseControl;

        public override void Start()
        {
            pauseControl = transform.parent.parent.parent.GetComponent<PauseOverlayController>();
            base.Start();
        }

        public override void Update()
        {
            ActivateOption();
            base.Update();
        }

        void ActivateOption()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.JoystickButton0) ||
                UnityEngine.Input.GetKeyDown(KeyCode.Return))
            {
                switch (cursorIndex)
                {
                    case 0:
                        pauseControl.Pause(false);
                        break;
                    case 1:
                        SceneManager.LoadScene("Loading");
                        break;
                    case 2:
                        PlayerManager.Reset();
                        SceneManager.LoadScene("Main Menu");
                        break;
                    default:
                        break;
                }
            }
        }

        
    }
}
