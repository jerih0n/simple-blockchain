using System.Collections.Generic;

namespace Blockchain.Node.CLI.Constants
{
    public class SupportedCommands
    {
        public const string SetNode = "node set";
        public const string RestoreNode = "node restore";
        public const string Help = "node help";
        public const string NodeDetails = "node info";
        public const string Synch = "node sync";
        public const string Start = "node run";

        public static readonly List<string> AllSupportedCommands = new List<string>
        {
            SetNode,
            RestoreNode,
            Help,
            NodeDetails,
            Synch,
            Start
        };
    }
}
