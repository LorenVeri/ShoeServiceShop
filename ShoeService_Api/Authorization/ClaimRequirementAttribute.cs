using HotChocolate.Types;
using Microsoft.AspNetCore.Mvc;
using ShoeService_Common.Constants;

namespace ShoeService_Api.Authorization
{
    public class ClaimRequirementAttribute : TypeFilterAttribute
    {
        public ClaimRequirementAttribute(FunctionCode functionId, CommandCode commandId)
            : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { functionId, commandId };
        }
    }
}
