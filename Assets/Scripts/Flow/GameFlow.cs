using System.Collections.Generic;
using UnityEngine;
using Garganta.Core;
using Garganta.Data;
using Garganta.Story;
using Garganta.Units;

namespace Garganta.Flow
{
    public enum FlowState { Title, Map, Base, Battle }

    // M3 meta flow: Title -> WorldMap <-> Bastion -> Battle (dialogue in/out) -> Map.
    public class GameFlow : MonoBehaviour
    {
        public static GameFlow Instance { get; private set; }
        public FlowState State = FlowState.Title;
        public int ActiveNode = -1;

        GameManager gm;
        DialogueUI dlg;
        TutorialManager tutor;

        void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
        }

        void Start()
        {
            gm = FindAnyObjectByType<GameManager>();
            dlg = FindAnyObjectByType<DialogueUI>();
            tutor = FindAnyObjectByType<TutorialManager>();
            EventBus.OnGameStateChanged += OnBattleState;
            State = FlowState.Title;
        }

        void OnDestroy() => EventBus.OnGameStateChanged -= OnBattleState;

        public void NewGame()
        {
            SaveSystem.NewGame();
            SaveSystem.LastSlot = 1;
            ActiveNode = -1;
            State = FlowState.Map;
        }

        public bool ContinueGame()
        {
            int s = SaveSystem.SlotExists(SaveSystem.LastSlot) ? SaveSystem.LastSlot : 1;
            return LoadSlot(s);
        }

        public bool LoadSlot(int s)
        {
            if (SaveSystem.Current != null && s == SaveSystem.LastSlot && State != FlowState.Title)
            {
                State = FlowState.Map;
                return true;
            }
            if (!SaveSystem.Load(s)) return false;
            ActiveNode = -1;
            State = FlowState.Map;
            return true;
        }

        public void ToMap() => State = FlowState.Map;
        public void ToBase() => State = FlowState.Base;
        public void ToTitle() => State = FlowState.Title;

        public void StartBattle(int node)
        {
            if (gm == null || dlg == null) return;
            ActiveNode = node;
            var cfg = ChapterDatabase.GetNode(node);
            State = FlowState.Battle;
            dlg.Play(cfg.Pre, () =>
            {
                gm.StartBattle(cfg);
                if (cfg.Tutorial && tutor != null) tutor.Begin();
            });
        }

        void OnBattleState(GameState s)
        {
            if (State != FlowState.Battle || ActiveNode < 0 || gm == null) return;
            var cfg = ChapterDatabase.GetNode(ActiveNode);
            if (s == GameState.Victory)
            {
                if (tutor != null) tutor.End();
                CaptureRoster();
                ApplyUnlocks(cfg);
                var save = SaveSystem.Current;
                if (save != null)
                {
                    save.progress = Mathf.Max(save.progress, ActiveNode + 1);
                    SaveSystem.CaptureRuntime();
                    SaveSystem.Save(SaveSystem.LastSlot);
                }
                dlg.Play(cfg.Post, () => State = FlowState.Map);
            }
            else if (s == GameState.Defeat)
            {
                if (tutor != null) tutor.End();
                // No persistent harm: next battle rebuilds from roster.
            }
        }

        void CaptureRoster()
        {
            var save = SaveSystem.Current;
            if (save == null) return;
            foreach (var u in gm.PlayerUnits)
            {
                var cap = u.Capture();
                int i = save.roster.FindIndex(r => r.rosterId == cap.rosterId);
                if (i >= 0) save.roster[i] = cap;
                else save.roster.Add(cap);
            }
        }

        void ApplyUnlocks(ChapterConfig cfg)
        {
            var save = SaveSystem.Current;
            if (save == null) return;
            foreach (var id in cfg.Unlocks)
                if (!save.roster.Exists(r => r.rosterId == id))
                    save.roster.Add(JoinTemplate(id));
        }

        static UnitSave JoinTemplate(string id)
        {
            switch (id)
            {
                case "Briar": return U("Briar", "Squire", 1, "hand_axe", "", "", "");
                case "Sera": return U("Sera", "Acolyte", 1, "wooden_staff", "", "", "");
                case "Voss": return U("Voss", "Archer", 2, "short_bow", "", "", "");
                default: return U(id, "Squire", 1, "rusty_sword", "", "", "");
            }
        }

        static UnitSave U(string id, string cls, int lv, string w, string a, string h, string acc)
            => new UnitSave
            {
                rosterId = id, classId = cls, level = lv, xp = 0,
                weaponId = w, armorId = a, helmetId = h, accId = acc,
                mastery = new List<MasteryEntry>(),
            };
    }
}
