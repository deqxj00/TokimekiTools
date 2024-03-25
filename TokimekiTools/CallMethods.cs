using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokimekiTools.DPK;
using TokimekiTools.PAK;

namespace TokimekiTools
{
    public class CallMethods
    {
        //DIRF
        public static void ExportDPKFile(string inputFile)
        {
            DIRFClass dirf= new DIRFClass();
            dirf.Read(inputFile);
        }

        public static void ExportPAKFile(string inputFile)
        {
            PAKClass dirf = new PAKClass();
            dirf.Read(inputFile);
        }
    }
}
