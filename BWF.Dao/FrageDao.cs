using BWF.Library;
using LAG.Framework.Dataaccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;

namespace BWF.Dao
{
    public class FrageDao : BaseDao
    {
        private string m_SqlReadFrageCol = "\r\nSELECT [FragenID] AS ID\r\n      ,[FragenGruppeID]\r\n      ,[FragenTypID]\r\n      ,[Frage]\r\n      ,[Hinweis]\r\n      ,[Sort]\r\n      ,[VorgabeID]\r\n      ,[Required]\r\n      ,[ValidationType]\r\n  FROM [Fragen]\r\n";
        private string m_SqlReadFrageColByGruppe = "\r\nSELECT [FragenID] AS ID\r\n      ,[FragenGruppeID]\r\n      ,[FragenTypID]\r\n      ,[Frage]\r\n      ,[Hinweis]\r\n      ,[Sort]\r\n      ,[VorgabeID]\r\n      ,[Required]\r\n      ,[ValidationType]\r\n  FROM [Fragen]\r\n  WHERE [FragenGruppeID] = '{0}'\r\n  ORDER BY [Sort]\r\n";
        private string m_SqlReadFragenGruppeCol = "\r\nSELECT [FragenGruppeID] AS ID\r\n      ,[Name]\r\n      ,[Sort]\r\n  FROM [FragenGruppe]\r\n  ORDER BY [Sort]\r\n";
        private string m_SqlReadAntwortColByFrage = "\r\nSELECT [AntwortID]  AS ID\r\n      ,[MöglicheAntwort]\r\n      ,[Sort]\r\n      ,[Zusatz]\r\n  FROM [Antworten]\r\n  WHERE [FragenID] = '{0}'\r\n  ORDER BY [Sort]\r\n";
        private string m_SqlSaveSoldatAntwortCol = "\r\nSELECT [SoldatAntwortID]        AS ID\r\n     ,[SoldatID]               \r\n     ,[FragenID]               AS [Fragen.ID]\r\n     ,[AntwortID]              AS [Antwort.AntwortID]\r\n     ,[TextAntwort]            \r\n     ,[AnlassID]\r\n FROM [SoldatAntwort]\r\nWHERE [SoldatID] = '{0}' AND [AnlassID] = '{1}'\r\n";

        public FrageDao(string connectionString)
            : base(connectionString)
        {
        }

        public FragenCol ReadFrageCol()
        {
            return this.Database.FillList<FragenCol>(new FragenCol(), typeof(Fragen), this.m_SqlReadFrageCol, (List<DbParameter>)null);
        }

        public FragenCol ReadFrageCol(Guid fragenGruppeId)
        {
            return this.Database.FillList<FragenCol>(new FragenCol(), typeof(Fragen), string.Format(this.m_SqlReadFrageColByGruppe, (object)fragenGruppeId), (List<DbParameter>)null);
        }

        public FragenGruppeCol ReadFragenGruppeCol()
        {
            return this.Database.FillList<FragenGruppeCol>(new FragenGruppeCol(), typeof(FragenGruppe), this.m_SqlReadFragenGruppeCol, (List<DbParameter>)null);
        }

        public AntwortCol ReadAntwortCol(Guid frageId)
        {
            return this.Database.FillList<AntwortCol>(new AntwortCol(), typeof(Antwort), string.Format(this.m_SqlReadAntwortColByFrage, (object)frageId), (List<DbParameter>)null);
        }

        public void SaveAntwortCol(SoldatAntwortCol soldatAntwortCol, Guid soldatId, Guid anlassId)
        {
            ParameterCollection parameters = new ParameterCollection();
            parameters.Add(new Parameter("SoldatID", (object)soldatId));
            parameters.Add(new Parameter("AnlassID", (object)anlassId));
            this.Database.SaveCollection((ICollection)soldatAntwortCol, string.Format(this.m_SqlSaveSoldatAntwortCol, (object)soldatId, (object)anlassId), (List<DbParameter>)null, parameters);
        }
    }
}