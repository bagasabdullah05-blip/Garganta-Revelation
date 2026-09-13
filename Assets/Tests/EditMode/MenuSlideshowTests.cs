using NUnit.Framework;
using Garganta.UI;

public class MenuSlideshowTests
{
    [Test]
    public void SlideIndex_Cycles()
    {
        Assert.AreEqual(0, TitleUI.SlideIndex(0f, 4, 8f));
        Assert.AreEqual(1, TitleUI.SlideIndex(8f, 4, 8f));
        Assert.AreEqual(2, TitleUI.SlideIndex(17f, 4, 8f));
        Assert.AreEqual(3, TitleUI.SlideIndex(24f, 4, 8f));
        Assert.AreEqual(0, TitleUI.SlideIndex(32f, 4, 8f)); // loops
        Assert.AreEqual(0, TitleUI.SlideIndex(5f, 0, 8f)); // no slides safe
    }

    [Test]
    public void SlideBlend_FadesAtEnd()
    {
        Assert.AreEqual(0f, TitleUI.SlideBlend(0f, 8f, 1.5f));
        Assert.AreEqual(0f, TitleUI.SlideBlend(6f, 8f, 1.5f));
        Assert.AreEqual(1f, TitleUI.SlideBlend(7.99f, 8f, 1.5f), 0.05f);
        Assert.AreEqual(0f, TitleUI.SlideBlend(8.1f, 8f, 1.5f), 0.05f); // new slot starts clean
    }
}
