using Blockchain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain.Node.Logic.Algorithms.Validation
{
    public class TransactionValidationService
    {
        public TransactionValidationService()
        {
        }

        public bool ValidateTransaction(Transaction transaction)
        {
            if (transaction == null)
            {
                return false;
            }
            if (transaction.TransactionType != TransactionTypesEnum.Normal)
            {
                return false;
            }

            return false;
        }
    }
}