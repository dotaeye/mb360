using System;
using System.Linq;
using AutoMapper;
using System.Reflection;
using SQ.Core.Task;
using SQ.Core.AutoMapper;

namespace MB.Data.AutoMapper
{
    public class AutoMapperStartupTask : IStartupTask
    {
        public void Execute()
        {
            Mapper.Initialize(x =>
            {
                // get all the AutoMapper Profile classes using reflection

                var profileTypes = Assembly.GetExecutingAssembly().GetTypes().Where(type => type.IsSubclassOf(typeof(BaseProfile)));

                foreach (var type in profileTypes)
                {
                    x.AddProfile((BaseProfile)Activator.CreateInstance(type));
                }
            });

            Mapper.AssertConfigurationIsValid();

        }

        public int Order
        {
            get { return 0; }
        }
    }
}
