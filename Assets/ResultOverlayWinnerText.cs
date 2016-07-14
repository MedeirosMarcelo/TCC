using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    using PlayerManager = Assets.Scripts.Game.PlayerManager;
    public class ResultOverlayWinnerText : MonoBehaviour
    {

        public Text title;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            var winners = PlayerManager.GetPlayerList().Where(player => player.Character.Lives > 0);
            if (winners.Count() > 0)
            {
                var winner = winners.First();
                title.text = "WINNER";
            }
            else
            {
                title.text = "NO CONTEST";
            }
            
        }
    }
}