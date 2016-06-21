using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;

using SharedExpenseManager.Expenses;

namespace SharedExpenseManager.DataStorageUtil
{
    public sealed class DataStorage // Prefferably a database online on a server, for current test state will save to file. Causes some limitations with datatype/interface usage
    {
        private static DataStorage s_dataStorageInstance;

        private const string c_storageFileName = @"data.sem";

        private string m_currentDirectory;

        private string m_saveFilePath;

        private List<Expense> ExpenseList;



        public DataStorage()
        {
            m_currentDirectory = System.AppDomain.CurrentDomain.BaseDirectory; // Get working dir. for exe, where storage file should be
            m_saveFilePath = Path.Combine(m_currentDirectory, c_storageFileName);
            SaveFileCheck();
        }

        public static DataStorage GetInstance
        {
            get
            {
                if (s_dataStorageInstance == null)
                {
                    s_dataStorageInstance = new DataStorage();
                }
                return s_dataStorageInstance;
            }
        }

        public StorageFile LoadStorageFile()
        {
            var storageFile = new StorageFile();
            if (new FileInfo(m_saveFilePath).Length == 0) // Check for file length, if zero then cannot read
            {
                return storageFile; // return new empty file
            }

            var deserializer = new DataContractSerializer(typeof(StorageFile));
            using (var reader = new FileStream(m_saveFilePath, FileMode.Open))
            {
                storageFile = (StorageFile)deserializer.ReadObject(reader);
            }
            return storageFile;
        }

        public void SaveStorageFile(StorageFile storageFile)
        {
            var serializer = new DataContractSerializer(typeof(StorageFile));
            using (var writer = new FileStream(m_saveFilePath, FileMode.Create))
            {
                serializer.WriteObject(writer, storageFile);
            }
        }

        private void SaveFileCheck() // Check for a save file, if present, use it, otherwise create a new one
        {
            if (!Directory.Exists(m_currentDirectory))
            {
                throw new DirectoryNotFoundException(); // Deal with this possibilty
            }
            
            if (!File.Exists(m_saveFilePath)) // No save file
            {
                File.Create(m_saveFilePath); // Create save file
            }
        }
    }
}
