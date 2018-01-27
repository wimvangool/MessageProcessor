﻿using System;
using System.Security.Principal;
using System.Threading.Tasks;
using Kingo.Resources;

namespace Kingo.Messaging.Authorization
{
    internal sealed class RequiresAuthenticatedPrincipalFilter : MicroProcessorFilterDecorator<AuthorizationFilterAttribute>
    {
        public RequiresAuthenticatedPrincipalFilter(AuthorizationFilterAttribute nextFilter) :
            base(nextFilter) { }

        protected override Task<TResult> HandleOrExecuteAsync<TResult>(MessagePipeline<TResult> pipeline, IMicroProcessorContext context)
        {
            if (NextFilter.Principal.Identity.IsAuthenticated)
            {
                return pipeline.InvokeNextFilterAsync(context);
            }
            throw NewPrincipalNotAuthenticatedException(NextFilter.Principal.Identity, context.Messages.Current.Message);            
        }

        private static Exception NewPrincipalNotAuthenticatedException(IIdentity identity, object failedMessage)
        {
            var messageFormat = ExceptionMessages.RequiresAuthenticatedPrincipalFilter_PrincipalNotAuthenticated;
            var message = string.Format(messageFormat, identity.Name);
            return new UnauthorizedRequestException(failedMessage, message);
        }
    }
}
