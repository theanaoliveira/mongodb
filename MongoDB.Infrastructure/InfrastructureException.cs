using System;

namespace MongoDB.Infrastructure
{
    public class InfrastructureException: Exception
    {
        public InfrastructureException(string businessMessage)
               : base(businessMessage)
        { }
    }
}
