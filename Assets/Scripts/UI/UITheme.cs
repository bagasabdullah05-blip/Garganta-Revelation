using UnityEngine;

namespace Garganta.UI
{
    // Keputusan font (OFL, di Assets/Resources/Fonts):
    // Display = Cinzel Decorative Bold (judul/menu — gothic emas era logo),
    // Body = Spectral Regular/Bold (dialogue/teks — serif terbaca kecil).
    public static class UITheme
    {
        public static readonly Color Gold = new Color(0.77f, 0.64f, 0.27f);
        public static readonly Color Bone = new Color(0.91f, 0.86f, 0.78f);
        public static readonly Color Dim = new Color(0.60f, 0.58f, 0.66f);

        static Font display;
        static Font body;
        static Font bodyBold;
        static GUIStyle menuButton;
        static GUIStyle bodyLabel;
        static GUIStyle speakerLabel;
        static GUIStyle centerLabel;

        public static Font Display()
        {
            if (display == null) display = Resources.Load<Font>("Fonts/CinzelDecorative-Bold");
            return display;
        }

        public static Font Body()
        {
            if (body == null) body = Resources.Load<Font>("Fonts/Spectral-Regular");
            return body;
        }

        public static Font BodyBold()
        {
            if (bodyBold == null) bodyBold = Resources.Load<Font>("Fonts/Spectral-Bold");
            return bodyBold;
        }

        public static GUIStyle MenuButton()
        {
            if (menuButton == null)
            {
                menuButton = BaseButton();
                if (Display() != null) menuButton.font = Display();
                menuButton.fontSize = 16;
                menuButton.normal.textColor = Gold;
                menuButton.hover.textColor = Color.white;
                menuButton.focused.textColor = Gold;
                menuButton.active.textColor = Color.white;
            }
            return menuButton;
        }

        public static GUIStyle BodyText()
        {
            if (bodyLabel == null)
            {
                bodyLabel = BaseLabel();
                if (Body() != null) bodyLabel.font = Body();
                bodyLabel.fontSize = 14;
                bodyLabel.normal.textColor = Bone;
                bodyLabel.wordWrap = true;
            }
            return bodyLabel;
        }

        public static GUIStyle Speaker()
        {
            if (speakerLabel == null)
            {
                speakerLabel = BaseLabel();
                if (BodyBold() != null) speakerLabel.font = BodyBold();
                speakerLabel.fontSize = 15;
            }
            return speakerLabel;
        }

        public static GUIStyle Center(int size = 14)
        {
            if (centerLabel == null) centerLabel = BaseLabel();
            centerLabel.alignment = TextAnchor.MiddleCenter;
            centerLabel.fontSize = size;
            if (Body() != null) centerLabel.font = Body();
            centerLabel.normal.textColor = Bone;
            return centerLabel;
        }

        public static void Clear()
        {
            display = body = bodyBold = null;
            menuButton = bodyLabel = speakerLabel = centerLabel = null;
        }

        // Headless-safe: GUI.skin is null outside the GUI loop (EditMode tests).
        static GUIStyle BaseButton()
        {
            try { if (GUI.skin != null && GUI.skin.button != null) return new GUIStyle(GUI.skin.button); }
            catch (System.Exception) { }
            var s = new GUIStyle();
            s.alignment = TextAnchor.MiddleCenter;
            return s;
        }

        static GUIStyle BaseLabel()
        {
            try { if (GUI.skin != null && GUI.skin.label != null) return new GUIStyle(GUI.skin.label); }
            catch (System.Exception) { }
            var s = new GUIStyle();
            s.alignment = TextAnchor.UpperLeft;
            return s;
        }
    }
}
