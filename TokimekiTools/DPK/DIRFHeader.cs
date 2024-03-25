using AdvancedBinary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokimekiTools.DPK
{
    internal class DIRFHeader
    {
        //44 49 52 46 
        [FString(Length = 4)]
        public string Signature;
        //A0 00 00 00
        public uint UnknowInt1;
        //00 00 08 00 
        public uint UnknowInt2;
        //00 00 00 16
        public uint FileCount;
        //00 00 08 00
        public uint UnknowInt3;
        //00 00 00 18 
        public uint UnknowInt4;
        //00 00 00 00 
        public uint UnknowInt5;
        //00 00 00 00 
        public uint UnknowInt6;


    }
}
