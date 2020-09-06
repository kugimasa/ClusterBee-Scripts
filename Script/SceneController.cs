using System.Collections.Generic;
using System.Linq;

static public class SceneController
{
    public enum SCENE { Home, Rule, Ranking, SelectLevel, Play, Result }
    public static SCENE scene = SCENE.Home;

    static Dictionary<string, SCENE> sceneDict = new Dictionary<string, SCENE>()
    {
        { "Home", SCENE.Home },
        { "Rule", SCENE.Rule },
        { "Ranking", SCENE.Ranking},
        { "SelectLevel", SCENE.SelectLevel},
        { "Play", SCENE.Play },
        { "Result", SCENE.Result },
    };

    public static string GetSceneName()
    {
        string sceneName = sceneDict.First(x => x.Value == scene).Key;
        return sceneName;
    }

    public static void ChangeScene(string sceneName)
    {
        scene = sceneDict[sceneName];
    }

}
