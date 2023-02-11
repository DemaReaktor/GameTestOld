using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using GameArchitecture;

public class SaveManagerTests
{
    //ISaveManager saveManager;

    //[SetUp]
    //public void SetUp()
    //{
    //    saveManager = new SaveManager();
    //    saveManager.Initialize(null);
    //}
    //[Test]
    //public void SetAndGetString()
    //{
    //    string y = "sd;agh";
    //    saveManager.Set(y, "string");
    //    string result;

    //    saveManager.Save();
    //    Assert.IsTrue(saveManager.TryGet(out result, "string"));
    //    Assert.AreEqual(y, result);
    //}
    //[Test]
    //public void SetAndGetInt()
    //{
    //    int y = 6;
    //    saveManager.Set(y,"int");
    //    string result;

    //    saveManager.Save();
    //    Assert.IsTrue(saveManager.TryGet(out result,"int"));
    //    Assert.AreEqual(y,int.Parse(result));
    //}
    [Test]
    public void SetAndGetInt()
    {
        Assert.AreEqual(typeof(IManager).Name,typeof(IManager<Game>).Name.Substring(0, typeof(IManager<Game>).Name.IndexOf('`')));
    }
}
