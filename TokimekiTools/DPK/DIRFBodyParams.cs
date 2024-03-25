using AdvancedBinary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokimekiTools.DPK
{
    internal class DIRFBodyParams
    {
        //4B 4F 4E 41 4D 49 2E 54 49 4D 00 00 
        //00 00 08 00 
        //00 02 58 14 
        //32 41 46 C3
        [FString(Length = 12)]
        public string FileName;
        public uint FileStartPos;
        public uint FileSize;
        public uint FileCrc32;//bzip crc32
             //using ICSharpCode.SharpZipLib.Checksum;
            //// 返回校验和
            //BZip2Crc bZip2Crc = new BZip2Crc();
            //bZip2Crc.Update(bytes);
            //var dd = (uint)bZip2Crc.Value;


    }
}
