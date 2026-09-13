using NUnit.Framework;
using Garganta.Art;

public class UiArtTests
{
    [Test]
    public void Missing_ReturnsNull()
    {
        UiArt.Clear();
        Assert.IsNull(UiArt.Icon("nope_missing"));
        Assert.IsNull(UiArt.Portrait("Nobody"));
        Assert.IsNull(UiArt.Bg("nowhere"));
        Assert.IsNull(UiArt.Icon(""));
        Assert.IsNull(UiArt.Portrait(null));
    }

    [Test]
    public void Cache_MissCachedAsNull()
    {
        UiArt.Clear();
        Assert.IsNull(UiArt.Icon("Attack"));
        Assert.IsNull(UiArt.Icon("Attack")); // second call hits cache, still null-safe
    }
}
