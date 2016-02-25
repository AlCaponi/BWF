
using BWF.Library;
using LAG.Framework.Dataaccess;
using System.Collections.Generic;
using System.Data.Common;

namespace BWF.Dao
{
    public class SoldatDao : BaseDao
    {
        private string m_SqlReadSoldat = "\r\nSELECT [SoldatID] AS ID\r\n      ,[SVNummer]\r\n      ,[Nachname]\r\n      ,[Vorname]\r\n      ,[Geburtsdatum]\r\n      ,Anlass.[AnlassID]   AS [Anlass.ID]\r\n      ,Anlass.Datum\r\n      ,Anlass.Name\r\n      ,Anlass.Zeit\r\n  FROM [Soldat]\r\n  INNER JOIN Anlass ON Anlass.AnlassID = Soldat.AnlassID\r\n    WHERE [SVNummer] = '{0}'\r\n";

        public SoldatDao(string connectionString)
            : base(connectionString)
        {
        }

        public Soldat ReadSoldat(string svNumber)
        {
            return this.Database.FillObject<Soldat>(new Soldat(), string.Format(this.m_SqlReadSoldat, (object)svNumber), (List<DbParameter>)null);
        }
    }
}
