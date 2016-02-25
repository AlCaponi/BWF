using LAG.Framework.Objects;

namespace BWF.Library
{
    public class FragenGruppe : ObjectBase
    {
        private FragenCol m_FragenCol = new FragenCol();
        private string m_Name;
        private int m_Sort;

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

        public FragenCol FragenCol
        {
            get
            {
                return this.m_FragenCol;
            }
            set
            {
                this.m_FragenCol = value;
            }
        }
    }

    public class FragenGruppeCol : CollectionBase<FragenGruppe>
    {
    }
}
