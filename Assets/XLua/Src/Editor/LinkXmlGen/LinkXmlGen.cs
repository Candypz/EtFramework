using UnityEngine;
using System.Collections.Generic;
using XLua;
using System.IO;
using System.Text;
using System.Linq;
using CSObjectWrapEditor;

public class LinkXmlGen : ScriptableObject
{
    public TextAsset Template;

    public static IEnumerable<CustomGenTask> GetTasks(LuaEnv lua_env, UserConfig user_cfg)
    {
        LuaTable data = lua_env.NewTable();
        var assembly_infos = (from type in user_cfg.ReflectionUse
                              group type by type.Assembly.GetName().Name into assembly_info
                              select new { FullName = assembly_info.Key, Types = assembly_info.ToList()}).ToList();
        data.Set("assembly_infos", assembly_infos);

        yield return new CustomGenTask
        {
            Data = data,
            Output = new StreamWriter(GeneratorConfig.common_path + "/link.xml",
            false, Encoding.UTF8)
        };
    }

    private static void copyDir(string fromDir, string toDir) {
        if (Directory.Exists(toDir)) {
            Directory.Delete(toDir, true);
        }
        Directory.CreateDirectory(toDir);

        if (!Directory.Exists(fromDir)) {
            Directory.CreateDirectory(fromDir);
        }

        string[] files = Directory.GetFiles(fromDir);
        foreach (string formFileName in files) {
            string fileName = Path.GetFileName(formFileName);
            string toFileName = Path.Combine(toDir, fileName);
            toFileName = toFileName.Replace(".lua", ".lua.bytes");
            File.Copy(formFileName, toFileName);
        }
        string[] fromDirs = Directory.GetDirectories(fromDir);
        foreach (string fromDirName in fromDirs) {
            string dirName = Path.GetFileName(fromDirName);
            string toDirName = Path.Combine(toDir, dirName);
            copyDir(fromDirName, toDirName);
        }
    }

    [GenCodeMenu]//加到Generate Code菜单里头
    public static void GenLinkXml()
    {
        copyDir(Application.dataPath + "/Lua", Application.dataPath + "/Resources/Lua");
        Generator.CustomGen(ScriptableObject.CreateInstance<LinkXmlGen>().Template.text, GetTasks);
    }
}
