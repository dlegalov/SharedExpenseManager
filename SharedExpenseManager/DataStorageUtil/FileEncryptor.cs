using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Text;

namespace SharedExpenseManager.DataStorageUtil
{
    public sealed class FileEncryptor // If using local storage, encrypt the files using DES or other to prevent private data from leaking
    {
        private const string c_password = "";

    }
}
