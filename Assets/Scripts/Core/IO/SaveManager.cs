using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DroidDigital.Core.Constants;

namespace DroidDigital.Core.IO
{
    public static class SaveManager
    {
        public static void Save<T>(T savefile)
        {
            try
            {
                var binaryFormatter = new BinaryFormatter();

                ValidateDirectory();
            
                FileStream file = new FileStream(GameConstants.FULL_SAVE_PATH, FileMode.Create);
            
                binaryFormatter.Serialize(file, savefile);
                
                file.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error ocurred while trying save: " + e);
                throw;
            }          
        }

        public static T Load<T>()
        {
            ValidateDirectory();
            
            if(!SaveExists()) throw new FileNotFoundException("file save does not exist");
            
            var binaryFormatter = new BinaryFormatter();
            
            FileStream file = new FileStream(GameConstants.FULL_SAVE_PATH, FileMode.Open);

            var saveFile = binaryFormatter.Deserialize(file);
            
            file.Close();

            return (T)saveFile;
        }

        public static void ValidateDirectory()
        {
            if (!Directory.Exists(GameConstants.SAVE_PATH))
                Directory.CreateDirectory(GameConstants.SAVE_PATH);
        }

        public static bool SaveExists()
        {
            return File.Exists(GameConstants.FULL_SAVE_PATH);
        }           
    }
}