using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.IO.Compression;
using System;

public class Import : MonoBehaviour
{
    public void OpenFile()
    {
        //string path = EditorUtility.OpenFilePanel("Overwrite with png", "", "apkg");
        //Debug.Log(path);
        //File.Move(path, Path.ChangeExtension(path, ".zip"));
        //string filename = Path.GetFileName(path);
        //SearchFile(path);

    }

    //void SearchFile(string a)
    //{
    //    using (ZipArchive zip = ZipFile.Open(a, ZipArchiveMode.Read))
    //        foreach (ZipArchiveEntry entry in zip.Entries)
    //            if (entry.FullName.EndsWith(".anki2", StringComparison.OrdinalIgnoreCase))
    //                entry.ExtractToFile(Application.dataPath + "/ImportData");
    //}
}