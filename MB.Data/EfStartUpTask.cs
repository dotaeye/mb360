using System;
using SQ.Core;
using SQ.Core.Ioc;
using SQ.Core.Infrastructure;
using SQ.Core.Task;
using SQ.Core.Data;

namespace MB.Data
{
    public class EfStartUpTask : IStartupTask
    {
        public void Execute()
        {
            var provider = EngineContext.Current.Resolve<IDataProvider>();
            if (provider == null)
                throw new Exception("No IDataProvider found");
            provider.SetDatabaseInitializer();

        }

        public int Order
        {
            //ensure that this task is run first 
            get { return -1000; }
        }
    }
}
