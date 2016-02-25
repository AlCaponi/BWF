using BWF.Library;
using LAG.Framework.Wcf;

namespace BWF
{
    public class BWFApplication
    {
        private static BWFApplication m_Application;
        private BWF.ServiceFactory.ServiceFactory m_ServiceFactory = new BWF.ServiceFactory.ServiceFactory();

        public static BWFApplication Current
        {
            get
            {
                if (m_Application == null)
                {
                    m_Application = new BWFApplication();
                }

                return m_Application;
            }
        }

        public T Services<T>() where T : class
        {
            return this.m_ServiceFactory.CreateService<T>();
        }
    }
}