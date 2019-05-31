using System;
using System.Collections.Generic;
using System.Linq;
using Aquiris.PacMan.IO;
using UnityEngine;

namespace Aquiris.LuckyWheel.Services
{
    public static class JsonSave<T>
    {
        public static void LoadSave(ref T targetFile)
        {
            if(!FileHelper.SaveDirectoryExists()) return;
            
            if(targetFile == null) throw new NullReferenceException("Save is null");

            targetFile = JsonUtility.FromJson<T>(FileHelper.LoadSaveFile());
        }

        public static void SaveGame(T targetSave)
        {
            if(targetSave == null) throw new NullReferenceException();

            var serializedJson = JsonUtility.ToJson(targetSave);
            
            FileHelper.WriteSaveFile(serializedJson);
        }
        
        public static void LoadWaysDirections(ref List<T> targetList)
        {
            if(!FileHelper.WayDirectoryExists()) return;
                             
            targetList = JsonUtility.FromJson<T[]>(FileHelper.LoadWayFile()).ToList();
        }

        
        public static void SaveWayDirections(List<T> targetList)
        {
            if(targetList.Count == 0) return;

            var serializedJson = JsonUtility.ToJson(targetList);
            
            FileHelper.WriteWayFile(serializedJson);
        }
    }
}