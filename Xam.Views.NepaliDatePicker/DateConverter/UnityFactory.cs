using DateConverter.Core;
using DateConverter.Core.Library;
using System;
using System.Collections.Generic;
using System.Text;
using Unity;

namespace DateConverter.Core
{
    public class UnityFactory
    {
        IUnityContainer container = new UnityContainer();
        private static IUnityContainer _container = null;
        public static IUnityContainer getUnityContainer()
        {
            if (_container == null)
            {
                UnityFactory unity = new UnityFactory();
                unity.createContainer();
            }
            return _container;
        }
        private void createContainer()
        {
            IUnityContainer container = new UnityContainer();
            registerServices(container);
            _container = container;
        }

        private void registerServices(IUnityContainer container)
        {
            container.RegisterType<iConversionStartDateData, ConversionStartDate5YearsInterval>();
            container.RegisterType<iDateConverter, DateConverter.Core.Library.DateConverter>();
            container.RegisterType<iDateFunctions, DateFunctions>();
            container.RegisterType<iFiscalYearFunctions, FiscalYearFunctions>();
            container.RegisterType<iNepaliDateData, NepaliDateData>();
        }
       
    }
}
