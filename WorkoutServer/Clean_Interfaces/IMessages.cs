using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkoutServer.Messages
{

    /// <summary>
    /// Interface to represent a request with a response
    /// </summary>
    /// <typeparam name="TResponse">Response type</typeparam>
    public interface IRequest<TResponse>
    {
    }
}