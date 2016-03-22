using BWF.Dao;
using BWF.Library;
using BWF.Service.Interface;
using LAG.Framework.Dataaccess;
using System;
using System.Collections.ObjectModel;

namespace BWF.Service
{
    public class BWFService : IBWFService
    {
        private FrageDao m_FrageDao;
        private SoldatDao m_SoldatDao;

        public BWFService()
        {
            try
            {
                //DaoFactory daoFactory = new DaoFactory("BrainWashFuck");
                this.m_FrageDao = new FrageDao("Password=B2RkphT2BAfVp8;User ID=db930888_admin;Persist Security Info=False;Initial Catalog=db930888;Data Source=sql3.agrinet.ch");
                this.m_SoldatDao = new SoldatDao("Password=B2RkphT2BAfVp8;User ID=db930888_admin;Persist Security Info=False;Initial Catalog=db930888;Data Source=sql3.agrinet.ch");
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Fehler in BWFService: {0}", ex.Message));
            }
        }

        public FragenCol GetFrageCol()
        {
            return this.m_FrageDao.ReadFrageCol();
        }

        public FragenGruppeCol GetFragenGruppeCol()
        {
            try
            {
                FragenGruppeCol fragenGruppeCol = this.m_FrageDao.ReadFragenGruppeCol();
                foreach (FragenGruppe fragenGruppe in (Collection<FragenGruppe>)fragenGruppeCol)
                {
                    fragenGruppe.FragenCol = this.m_FrageDao.ReadFrageCol(fragenGruppe.ID);
                    foreach (Fragen fragen in (Collection<Fragen>)fragenGruppe.FragenCol)
                        fragen.AntwortCol = this.m_FrageDao.ReadAntwortCol(fragen.ID);
                }
                return fragenGruppeCol;
            }
            catch (Exception ex)
            {
                throw new Exception("Fehler beim lesen der Fragen Gruppe Collection", ex);
            }
        }

        public Soldat GetSoldat(string svNumber)
        {
            try
            {
                return this.m_SoldatDao.ReadSoldat(svNumber);
            }
            catch (Exception ex)
            {
                throw new Exception("Fehler beim lesen des Soldaten");
            }
        }

        public void SaveSoldatAntwortCol(SoldatAntwortCol soldatAntwortCol, Guid soldatId, Guid anlassId)
        {
            try
            {
                this.m_FrageDao.SaveAntwortCol(soldatAntwortCol, soldatId, anlassId);
            }
            catch (Exception ex)
            {
                throw new Exception("Fehler beim speichern der Antworten", ex);
            }
        }
    }
}
