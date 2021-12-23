using Blockchain.Cryptography.Block;
using Blockchain.Cryptography.Extenstions;
using Blockchain.Utils;
using System;
using System.Collections;

namespace Blockchain.Node.Logic.Algorithms.Validation
{
    public static class BlockValidator
    {
        public static AfterBlockValidationAction ValidateRecievedBlock(Block previousBlock, Block newBlock)
        {
            if(previousBlock.Id >= newBlock.Id)
            {
                return AfterBlockValidationAction.Reject;
            }
            if(newBlock.Id - previousBlock.Id > 1)
            {
                return AfterBlockValidationAction.RequestChainSynchronization;
            }

            var lastBlockHash = previousBlock.BlockHash;
            var expectedNewBlockHash = BlockHash.CalculateBlockHash(lastBlockHash, newBlock.Nonce);
            var isValidBlockSolution = IsValidBlockSolution(expectedNewBlockHash, previousBlock.NextComplexity);
            if(!isValidBlockSolution)
            {
                return AfterBlockValidationAction.Reject;
            }

            if(newBlock.BlockHash != expectedNewBlockHash.ToHex())
            {
                return AfterBlockValidationAction.Reject;
            }
            return AfterBlockValidationAction.Accept;
        }

        public static bool IsValidBlockSolution(byte[] possibleBlockHash, int complexity)
        {
            if (complexity > 254)
            {
                throw new Exception("Invalid Complexity!");
            }
            BitArray bitArray = new BitArray(possibleBlockHash);
            bool isSolution = false;
            for (int i = 0; i < complexity; i++)
            {
                isSolution = bitArray[i];
                if (!isSolution)
                {
                    continue;
                    // basicaly isSolution is bit => true or false . In order to be a solution need to have 0 value or false
                };
                break;
            }
            return !isSolution;
        }
    }
}
