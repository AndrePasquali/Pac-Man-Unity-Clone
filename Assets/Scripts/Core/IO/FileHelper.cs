using System.IO;
using Aquiris.Core.Constants;

namespace Aquiris.PacMan.IO
{
    public static class FileHelper
    {
        #region Save

        public static bool SaveFileExists()
        {
            return System.IO.File.Exists(GameConstants.FULL_SAVE_PATH);
        }

        public static bool SaveDirectoryExists()
        {
            return Directory.Exists(GameConstants.FULL_SAVE_PATH);
        }

        public static void CreateSaveDirectory()
        {
            if(SaveDirectoryExists()) return;

            Directory.CreateDirectory(GameConstants.SAVE_PATH);
        }

        public static void WriteSaveFile(string stockFileToWrite)
        {
            if(!SaveDirectoryExists())
                CreateSaveDirectory();

            var filewriter = new StreamWriter(GameConstants.FULL_SAVE_PATH);
            
            filewriter.Write(stockFileToWrite);
            
            filewriter.Flush();
            
            filewriter.Close();
        }

        public static string LoadSaveFile()
        {
            return System.IO.File.ReadAllText(GameConstants.FULL_SAVE_PATH);
        }
        
        #endregion

        #region Path

        public static bool WayFileExists()
        {
            return System.IO.File.Exists(GameConstants.WAY_DIRECTIONS_FULL_SAVE_PATH);
        }

        public static bool WayDirectoryExists()
        {
            return Directory.Exists(GameConstants.WAY_DIRECTIONS_PATH);
        }

        public static void CreateWayDirectory()
        {
            if(WayDirectoryExists()) return;

            Directory.CreateDirectory(GameConstants.WAY_DIRECTIONS_PATH);
        }

        public static void WriteWayFile(string log)
        {
            if(!WayDirectoryExists())
                CreateWayDirectory();
            
            var filewriter = new StreamWriter(GameConstants.WAY_DIRECTIONS_FULL_SAVE_PATH);
            
            filewriter.Write(log);
            
            filewriter.Flush();
            
            filewriter.Close();
        }
        
        public static string LoadWayFile()
        {
            return System.IO.File.ReadAllText(GameConstants.WAY_DIRECTIONS_FULL_SAVE_PATH);
        }

        
        #endregion
               
    }
}