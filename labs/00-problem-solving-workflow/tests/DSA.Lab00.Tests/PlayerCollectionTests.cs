using System.Collections.Generic;
using DSA.Lab00;
using NUnit.Framework;

namespace DSA.Lab00.Tests;

[TestFixture]
public sealed class PlayerCollectionTests
{
    [Test]
    public void FindById_ReturnsMatchingPlayer()
    {
        PlayerCollection collection = CreateCollection();

        Player? player = collection.FindById(734);

        Assert.That(player, Is.Not.Null);
        Assert.That(player!.Name, Is.EqualTo("Cleo"));
    }

    [Test]
    public void FindById_ReturnsNullWhenIdDoesNotExist()
    {
        PlayerCollection collection = CreateCollection();

        Player? player = collection.FindById(999);

        Assert.That(player, Is.Null);
    }

    [Test]
    public void FindHighestScore_ReturnsPlayerWithHighestScore()
    {
        PlayerCollection collection = CreateCollection();

        Player? player = collection.FindHighestScore();

        Assert.That(player, Is.Not.Null);
        Assert.That(player!.Id, Is.EqualTo(734));
    }

    [Test]
    public void FindHighestScore_ReturnsNullForEmptyCollection()
    {
        PlayerCollection collection = new(new List<Player>());

        Player? player = collection.FindHighestScore();

        Assert.That(player, Is.Null);
    }

    [Test]
    public void ContainsName_ReturnsTrueWhenNameExists()
    {
        PlayerCollection collection = CreateCollection();

        bool contains = collection.ContainsName("Bo");

        Assert.That(contains, Is.True);
    }

    [Test]
    public void ContainsName_ReturnsFalseWhenNameDoesNotExist()
    {
        PlayerCollection collection = CreateCollection();

        bool contains = collection.ContainsName("Eli");

        Assert.That(contains, Is.False);
    }

    private static PlayerCollection CreateCollection()
    {
        return new PlayerCollection(new List<Player>
        {
            new Player(101, "Amina", 120),
            new Player(205, "Bo", 85),
            new Player(734, "Cleo", 160),
            new Player(910, "Dani", 105)
        });
    }
}
