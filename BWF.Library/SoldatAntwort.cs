using LAG.Framework.Objects;

namespace BWF.Library
{
    public class SoldatAntwort : ObjectBase
    {
        private Soldat m_Soldat = new Soldat();
        private Fragen m_Fragen = new Fragen();
        private Antwort m_Antwort = new Antwort();
        private Anlass m_Anlass = new Anlass();
        private string m_TextAntwort;

        public Soldat Soldat
        {
            get
            {
                return this.m_Soldat;
            }
            set
            {
                this.m_Soldat = value;
            }
        }

        public Fragen Fragen
        {
            get
            {
                return this.m_Fragen;
            }
            set
            {
                this.m_Fragen = value;
            }
        }

        public Antwort Antwort
        {
            get
            {
                return this.m_Antwort;
            }
            set
            {
                this.m_Antwort = value;
            }
        }

        public Anlass Anlass
        {
            get
            {
                return this.m_Anlass;
            }
            set
            {
                this.m_Anlass = value;
            }
        }

        public string TextAntwort
        {
            get
            {
                return this.m_TextAntwort;
            }
            set
            {
                this.m_TextAntwort = value;
            }
        }
    }

    public class SoldatAntwortCol : CollectionBase<SoldatAntwort>
    {
    }
}
