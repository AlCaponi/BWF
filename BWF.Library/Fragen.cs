using LAG.Framework.Objects;
using System;

namespace BWF.Library
{
    public class Fragen : ObjectBase
    {
        private AntwortCol m_AntwortCol = new AntwortCol();
        private string m_Frage;
        private string m_Hinweis;
        private int m_Sort;
        private Guid m_FragenTyp;
        private bool m_Required;
        private string m_ValidationType;

        public string Frage
        {
            get
            {
                return this.m_Frage;
            }
            set
            {
                this.m_Frage = value;
            }
        }

        public string Hinweis
        {
            get
            {
                return this.m_Hinweis;
            }
            set
            {
                this.m_Hinweis = value;
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

        public AntwortCol AntwortCol
        {
            get
            {
                return this.m_AntwortCol;
            }
            set
            {
                this.m_AntwortCol = value;
            }
        }

        public Guid FragenTypID
        {
            get
            {
                return this.m_FragenTyp;
            }
            set
            {
                this.m_FragenTyp = value;
            }
        }

        public bool Required
        {
            get
            {
                return this.m_Required;
            }
            set
            {
                this.m_Required = value;
            }
        }

        public string ValidationType
        {
            get
            {
                return this.m_ValidationType;
            }
            set
            {
                this.m_ValidationType = value;
            }
        }
    }

    public class FragenCol : CollectionBase<Fragen>
    {
    }
}