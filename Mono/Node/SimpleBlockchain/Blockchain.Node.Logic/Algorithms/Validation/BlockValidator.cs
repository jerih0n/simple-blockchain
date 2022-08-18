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
            if (previousBlock.BlockHeader.Id >= newBlock.BlockHeader.Id)
            {
                return AfterBlockValidationAction.Reject;
            }
            if (newBlock.BlockHeader.Id - previousBlock.BlockHeader.Id > 1)
            {
                return AfterBlockValidationAction.RequestChainSynchronization;
            }

            var lastBlockHash = previousBlock.BlockHeader.BlockHash;
            var expectedNewBlockHash = BlockHash.CalculateBlockHash(lastBlockHash, newBlock.BlockHeader.Nonce);
            var isValidBlockSolution = IsValidBlockSolution(expectedNewBlockHash, previousBlock.BlockHeader.NextComplexity);
            if (!isValidBlockSolution)
            {
                return AfterBlockValidationAction.Reject;
            }

            if (newBlock.BlockHeader.BlockHash != expectedNewBlockHash.ToHex())
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