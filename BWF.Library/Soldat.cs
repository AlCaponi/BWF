using LAG.Framework.Objects;
using System;

namespace BWF.Library
{
    public class Soldat : ObjectBase
    {
        private Anlass m_Anlass = new Anlass();
        private string m_SVNummer;
        private string m_Nachname;
        private string m_Vorname;
        private DateTime m_Geburtsdatum;

        public string SVNummer
        {
            get
            {
                return this.m_SVNummer;
            }
            set
            {
                this.m_SVNummer = value;
            }
        }

        public string Nachname
        {
            get
            {
                return this.m_Nachname;
            }
            set
            {
                this.m_Nachname = value;
            }
        }

        public string Vorname
        {
            get
            {
                return this.m_Vorname;
            }
            set
            {
                this.m_Vorname = value;
            }
        }

        public DateTime Geburtstdaum
        {
            get
            {
                return this.m_Geburtsdatum;
            }
            set
            {
                this.m_Geburtsdatum = value;
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
    }
}