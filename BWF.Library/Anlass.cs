using LAG.Framework.Objects;
using System;

namespace BWF.Library
{
    public class Anlass : ObjectBase
    {
        private string m_Name;
        private DateTime m_Datum;
        private TimeSpan m_Zeit;

        public string Name
        {
            get
            {
                return this.m_Name;
            }
            set
            {
                this.m_Name = value;
            }
        }

        public DateTime Datum
        {
            get
            {
                return this.m_Datum;
            }
            set
            {
                this.m_Datum = value;
            }
        }

        public TimeSpan Zeit
        {
            get
            {
                return this.m_Zeit;
            }
            set
            {
                this.m_Zeit = value;
            }
        }
    }

    public class AnlassCol : CollectionBase<Anlass>
    {
    }
}
