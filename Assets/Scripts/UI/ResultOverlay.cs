using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
    using PlayerManager = Game.PlayerManager;

    public class ResultOverlay : Menu
    {
        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            ActivateOption();
            base.Update();
        }

        void ActivateOption()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.JoystickButton0)
                || UnityEngine.Input.GetKeyDown(KeyCode.Return))
            {
                switch (cursorIndex)
                {
                    case 0:
                        SceneManager.LoadScene("Loading");
                        break;
                    case 1:
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
