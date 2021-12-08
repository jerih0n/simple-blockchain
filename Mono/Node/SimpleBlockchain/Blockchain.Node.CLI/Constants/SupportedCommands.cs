using System.Collections.Generic;

namespace Blockchain.Node.CLI.Constants
{
    public class SupportedCommands
    {
        public const string Help = "node help";
        public const string NodeDetails = "node info";
        public const string Synch = "node sync";
        public const string Start = "node run";

        public static readonly List<string> AllSupportedCommands = new List<string>
        {
            Help,
            NodeDetails,
            Synch,
            Start
        };
    }
}
