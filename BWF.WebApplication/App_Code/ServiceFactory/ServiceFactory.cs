using System;
using System.Collections.Generic;
using BWF.Library;
using BWF.Service;

namespace BWF.ServiceFactory
{
    public class ServiceFactory
    {
        private Dictionary<Type, object> m_CacheServices = new Dictionary<Type, object>();
        //private LAG.Framework.Wcf.ServiceFactory m_ServiceFactory = new LAG.Framework.Wcf.ServiceFactory(Const.BWF_CONFIG_CONTEXT);
        private LAG.Framework.Wcf.ServiceFactory m_ServiceFactory = new LAG.Framework.Wcf.ServiceFactory("BrainWashFuck");

        internal T CreateService<T>() where T : class
        {
            //Typ bestimmen
            Type type = typeof(T);

            try
            {
                // Prüfen ob bereits im Cache
                if (this.m_CacheServices.ContainsKey(type) == false)
                {

                    //T service = m_ServiceFactory.UseService<T>();
                    //Temporär weil nicht funktioniert mit Website
                    BWFService service = null;
                    if (type == typeof(BWF.Service.Interface.IBWFService))
                    {
                        service = new BWF.Service.BWFService();
                    }
                    // Wenn Service nicht gefunden -> Exception
                    if (service == null)
                    {
                        throw new ApplicationException();
                    }

                    // Service zu cache hinzufügen
                    this.m_CacheServices.Add(type, service);
                }

                return (T)this.m_CacheServices[type];
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("No implementation found for '{0}'", type.FullName), ex.InnerException);
            }
        }
    }
}