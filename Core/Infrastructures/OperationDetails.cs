using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infrastructures
{
    public class OperationDetails
    {
        public bool Succedeed { get; private set; }
        public string Message { get; private set; }
        public OperationDetails(bool succedeed, string message)
        {
            this.Succedeed = succedeed;
            this.Message = message;
        }
    }
}
