#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;

public class ExportSpriteSheet
{
    [MenuItem("Assets/导出图集子图为PNG", false, 30)]
    static void ExportSelectedSpriteSheet()
    {
        Texture2D tex = Selection.activeObject as Texture2D;
        if (tex == null)
        {
            EditorUtility.DisplayDialog("提示", "请选中一张Multiple模式的精灵图集", "OK");
            return;
        }

        string assetPath = AssetDatabase.GetAssetPath(tex);
        TextureImporter importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;
        if (importer == null || importer.spriteImportMode != SpriteImportMode.Multiple)
        {
            EditorUtility.DisplayDialog("提示", "选中图片不是Multiple精灵图集", "OK");
            return;
        }

        //输出目录：原图同目录下新建输出文件夹
        string dir = Path.GetDirectoryName(assetPath);
        string outDir = Path.Combine(Application.dataPath, dir.Substring("Assets/".Length), tex.name + "_Export");
        if (!Directory.Exists(outDir)) Directory.CreateDirectory(outDir);

        int count = 0;
        foreach (var meta in importer.spritesheet)
        {
            Rect r = meta.rect;
            int w = (int)r.width;
            int h = (int)r.height;
            Texture2D sprTex = new Texture2D(w, h, tex.format, false);
            //拷贝对应区域像素
            sprTex.SetPixels(tex.GetPixels((int)r.x, (int)r.y, w, h));
            sprTex.Apply();

            byte[] pngBytes = sprTex.EncodeToPNG();
            string savePath = Path.Combine(outDir, $"{meta.name}.png");
            File.WriteAllBytes(savePath, pngBytes);
            count++;
        }

        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("完成", $"导出 {count} 张图片\n{outDir}", "确定");
    }
}
#endif