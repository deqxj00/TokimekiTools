using AdvancedBinary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TokimekiTools.DPK;

namespace TokimekiTools.PAK
{
    internal class PAKClass
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
            var tmpBytes = Reader.ReadBytes(4).Reverse().ToArray();
            uint filecount = BitConverter.ToUInt32(tmpBytes);
            List<uint> listPos = new List<uint>();
            for (int i = 0; i < filecount; i++)
            {
                tmpBytes = Reader.ReadBytes(4).Reverse().ToArray();
                listPos.Add(BitConverter.ToUInt32(tmpBytes));
            }
            long pos = Reader.Position();
            for (int i = 0; i < listPos.Count; i++) {
                Reader.Seek(pos + listPos[i],SeekOrigin.Begin);
                long len = 0;
                if((i+1) == listPos.Count)
                {
                    len = OrgBytes.LongLength - listPos[i];
                }
                else
                {
                    len = listPos[i + 1] - listPos[i];
                }
                var bytes = Reader.ReadBytes((int)len);
                string filename = dinfo.Name.Replace(dinfo.Extension, "") + "_" + i.ToString("D2");
                File.WriteAllBytes(Path.Combine(UnpackBasePath, filename), bytes);
                Console.WriteLine("Unpacked:" + filename);
            }

            //foreach (var param in @params)
            //{
            //    Reader.Seek(param.FileStartPos, SeekOrigin.Begin);
            //    var bytes = Reader.ReadBytes((int)param.FileSize);
            //    File.WriteAllBytes(Path.Combine(UnpackBasePath, param.FileName), bytes);
            //    Console.WriteLine("Unpacked:" + param.FileName);
            //}
        }

        public void Write(string filepath)
        {

        }
    }
}
