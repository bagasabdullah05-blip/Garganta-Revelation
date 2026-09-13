using NUnit.Framework;
using UnityEngine;
using Garganta.UI;

public class UIThemeTests
{
    [Test]
    public void Fonts_Load()
    {
        UITheme.Clear();
        Assert.NotNull(UITheme.Display(), "CinzelDecorative-Bold");
        Assert.NotNull(UITheme.Body(), "Spectral-Regular");
        Assert.NotNull(UITheme.BodyBold(), "Spectral-Bold");
    }

    [Test]
    public void Styles_Built()
    {
        UITheme.Clear();
        Assert.AreEqual(22, UITheme.MenuButton().fontSize);
        Assert.AreEqual(18, UITheme.BodyText().fontSize);
        Assert.AreEqual(20, UITheme.Speaker().fontSize);
        Assert.AreEqual(TextAnchor.MiddleCenter, UITheme.Center().alignment);
    }
}
