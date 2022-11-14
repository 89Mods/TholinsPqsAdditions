using System;
using System.Linq;
using UnityEngine;

namespace TholinsPQSAdditions
{
    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class TholinPQSAddon : MonoBehaviour
    {
        private Game game;

        public void Start()
        {
            Debug.Log("[Tholin] Hewwo there uwu");
        }

        private void setGame(Game g)
        {
            game = g;
        }

    }
}

