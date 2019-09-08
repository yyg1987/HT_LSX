using Abp.Dependency;
using Abp.Events.Bus.Exceptions;
using Abp.Events.Bus.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LY.PF
{
    public class ExceptionEventHandler : IEventHandler<AbpHandledExceptionData>, ITransientDependency
    {
        /// <summary>
        /// Handler handles the event by implementing this method.
        /// </summary>
        /// <param name="eventData">Event data</param>
        public void HandleEvent(AbpHandledExceptionData eventData)
        {
            Console.WriteLine($"当前异常信息为：{eventData.Exception.Message}");
        }
    }
}
