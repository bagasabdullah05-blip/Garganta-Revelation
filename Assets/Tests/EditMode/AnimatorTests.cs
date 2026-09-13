using NUnit.Framework;
using UnityEngine;
using Garganta.Art;

public class AnimatorTests
{
    [Test]
    public void FrameAt_Loops()
    {
        Assert.AreEqual(0, UnitAnimator.FrameAt(0f, 8, 4, true));
        Assert.AreEqual(2, UnitAnimator.FrameAt(0.26f, 8, 4, true));
        Assert.AreEqual(0, UnitAnimator.FrameAt(0.5f, 8, 4, true)); // wraps
    }

    [Test]
    public void FrameAt_ClampsOneShot()
    {
        Assert.AreEqual(3, UnitAnimator.FrameAt(10f, 8, 4, false));
        Assert.AreEqual(0, UnitAnimator.FrameAt(0f, 8, 0, true)); // empty safe
    }

    [Test]
    public void StripCount_Math()
    {
        Assert.AreEqual(0, UnitAnimator.StripCount(null, 32));
        var tex = new Texture2D(128, 48);
        Assert.AreEqual(4, UnitAnimator.StripCount(tex, 32));
        Object.DestroyImmediate(tex);
    }

    [Test]
    public void MissingStrips_FallbackSilent()
    {
        var go = new GameObject("anim");
        try
        {
            var anim = go.AddComponent<UnitAnimator>();
            var tex = new Texture2D(32, 48);
            var baseSprite = Sprite.Create(tex, new Rect(0, 0, 32, 48), new Vector2(0.5f, 0.5f), 32f);
            anim.Setup("NoSuchUnit_XYZ", baseSprite);
            Assert.IsFalse(anim.HasClip(AnimClip.Walk));
            Assert.DoesNotThrow(() => anim.PlayWalk(true));
            Assert.DoesNotThrow(() => anim.PlayAttack());
            Assert.DoesNotThrow(() => anim.PlayHit());
            Assert.DoesNotThrow(() => anim.PlayDeath());
            Assert.DoesNotThrow(() => anim.SetFacing(-1f));
            Object.DestroyImmediate(tex);
        }
        finally { Object.DestroyImmediate(go); }
    }
}
