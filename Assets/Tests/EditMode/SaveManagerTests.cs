using NUnit.Framework;
using GameArchitecture.Save;

public class SaveManagerTests
{
    ISaveManager<SettingsConfiguration> saveManager;

    [SetUp]
    public void SetUp()
    {
        saveManager = new SettingsManager();
        saveManager.Initialize(new SettingsConfiguration() { FileName = "Settings"});
    }
    [Test]
    public void SetAndGetString()
    {
        string y = "sd;agh";
        saveManager.Set(y, "string");
        string result;

        saveManager.Save();
        Assert.IsTrue(saveManager.TryGet(out result, "string"));
        Assert.AreEqual(y, result);
    }
    [Test]
    public void SetAndGetInt()
    {
        int y = 6;
        saveManager.Set(y, "int");
        string result;

        saveManager.Save();
        Assert.IsTrue(saveManager.TryGet(out result, "int"));
        Assert.AreEqual(y, int.Parse(result));
    }
}
