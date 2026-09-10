using UnityEngine;
using Garganta.Core;

namespace Garganta.Story
{
    // Scripted hints for Ch.1 (Kael solo tutorial).
    public class TutorialManager : MonoBehaviour
    {
        bool active;
        int step;
        Vector2Int kaelStart;
        int dmgSeen;

        static readonly string[] hints =
        {
            "Giliran Kael (tanda *). Klik Kael.",
            "Klik tile biru untuk bergerak.",
            "Klik Attack, lalu klik Goblin merah.",
            "Bagus! Buka Skill > Power Strike untuk damage besar.",
            "Habisi semua musuh untuk menang!",
        };

        public void Begin()
        {
            active = true;
            step = 0;
            dmgSeen = 0;
            EventBus.OnDamageDealt += OnDmg;
            var kael = FindKael();
            kaelStart = kael != null ? kael.Coord : new Vector2Int(1, 1);
        }

        public void End()
        {
            active = false;
            EventBus.OnDamageDealt -= OnDmg;
        }

        void OnDmg(string a, int d, string b) => dmgSeen++;

        static Units.Unit FindKael()
        {
            var gm = GameManager.Instance;
            if (gm == null) return null;
            foreach (var u in gm.PlayerUnits)
                if (u.RosterId == "Kael") return u;
            return null;
        }

        void Update()
        {
            if (!active) return;
            var gm = GameManager.Instance;
            if (gm == null || gm.CurrentUnit == null) return;
            var kael = FindKael();
            switch (step)
            {
                case 0:
                    if (gm.SelectedUnit != null && gm.SelectedUnit.RosterId == "Kael") step = 1;
                    break;
                case 1:
                    if (kael != null && kael.Coord != kaelStart) step = 2;
                    break;
                case 2:
                    if (dmgSeen > 0) step = 3;
                    break;
                case 3:
                    if (dmgSeen > 1) step = 4;
                    break;
            }
        }

        void OnGUI()
        {
            if (!active) return;
            var r = new Rect(Screen.width / 2 - 220, 44, 440, 40);
            GUILayout.BeginArea(r, "box");
            GUILayout.Label($"TUTORIAL: {hints[Mathf.Min(step, hints.Length - 1)]}");
            GUILayout.EndArea();
        }
    }
}
