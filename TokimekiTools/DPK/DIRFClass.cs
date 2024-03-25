using AdvancedBinary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokimekiTools.DPK
{
    internal class DIRFClass
    {
        public void Read(string filepath)
        {
            FileInfo dinfo = new FileInfo(filepath);

            string UnpackBasePath = Path.Combine(dinfo.DirectoryName, dinfo.Name.Replace(dinfo.Extension, "_Unpack"));
            if (!Directory.Exists(UnpackBasePath))
            {
                Directory.CreateDirectory(UnpackBasePath);
            }
            var OrgBytes = File.ReadAllBytes(filepath);
            StructReader Reader = new StructReader(new MemoryStream(OrgBytes));
            //44 49 52 46 
            if (
                OrgBytes[0] == 0x44 &&
                OrgBytes[1] == 0x49 &&
                OrgBytes[2] == 0x52 &&
                OrgBytes[3] == 0x46
                )
            {
                Reader.BigEndian = true;
            }
            else if (
                OrgBytes[0] == 0x46 &&
                OrgBytes[1] == 0x52 &&
                OrgBytes[2] == 0x49 &&
                OrgBytes[3] == 0x44
                )
            {
                Reader.BigEndian = false;
            }

            DIRFHeader dirfHeader = new DIRFHeader();
            try
            {
                Reader.ReadStruct(ref dirfHeader);
                if (dirfHeader.Signature.StartsWith("DIRF"))
                {
                    Console.WriteLine(dinfo.Name+" DIRF");
                }
                else if (dirfHeader.Signature.StartsWith("FRID"))
                {
                    Console.WriteLine(dinfo.Name + " FRID");
                }
                else
                {
                    Console.WriteLine("非DIRF");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return;
            }

            DIRFBodyParams[] @params = new DIRFBodyParams[dirfHeader.FileCount];
            for (int i = 0; i < dirfHeader.FileCount; i++)
            {
                DIRFBodyParams dirfBodyParams = new DIRFBodyParams();
                Reader.ReadStruct(ref dirfBodyParams);
                dirfBodyParams.FileName = dirfBodyParams.FileName.Replace("\x0", "");
                @params[i] = dirfBodyParams;
            }

            foreach (var param in @params)
            {
                Reader.Seek(param.FileStartPos, SeekOrigin.Begin);
                var bytes = Reader.ReadBytes((int)param.FileSize);
                File.WriteAllBytes(Path.Combine(UnpackBasePath, param.FileName), bytes);
                Console.WriteLine("Unpacked:" + param.FileName);
            }


        }

        public void Write(string path) { 
        
        
        }  
    }
}
