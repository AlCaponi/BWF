using LAG.Framework.Objects;
using System;

namespace BWF.Library
{
    public class Antwort : ObjectBase
    {
        private string m_MöglicheAntwort;
        private int m_Sort;
        private string m_Zusatz;
        private Guid? m_AntwortID;

        public string MöglicheAntwort
        {
            get
            {
                return this.m_MöglicheAntwort;
            }
            set
            {
                this.m_MöglicheAntwort = value;
            }
        }

        public int Sort
        {
            get
            {
                return this.m_Sort;
            }
            set
            {
                this.m_Sort = value;
            }
        }

        public string Zusatz
        {
            get
            {
                return this.m_Zusatz;
            }
            set
            {
                this.m_Zusatz = value;
            }
        }

        public Guid? AntwortID
        {
            get
            {
                return this.m_AntwortID;
            }
            set
            {
                this.m_AntwortID = value;
            }
        }
    }

    public class AntwortCol : CollectionBase<Antwort>
    {
    }
}
