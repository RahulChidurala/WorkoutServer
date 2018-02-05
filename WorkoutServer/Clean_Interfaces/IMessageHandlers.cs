using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutServer.Messages;

namespace WorkoutServer.MessageHandlers
{
    interface IRequestHandler<TRequest, TResponse> where TRequest: IRequest<TResponse>
    {
        /// <summary>
        /// Handles a request
        /// </summary>
        /// <param name="request">The request message</param>
        /// <returns>Response from the request</returns>
        TResponse handle(TRequest request);
    }

    interface IResponseHandler<Response>
    {
        void handle(Response response);
    }
}
