using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeCenter.CommonDomain;

namespace HomeCenter.HistoryDomain
{
    public class HistoryLogRepository : GenericRepository<HistoryLog>, IHistoryLogRepository
    {
        public HistoryLogRepository(IDbContext contextDb) : base(contextDb)
        {
        }

        public void AddLog(HistoryLog historyLog)
        {
            _set.Add(historyLog);
        }

        public List<HistoryLog> GetAllLogs()
        {
            return _set.OrderByDescending(l => l.DateTime).ToList();
        }

        public List<HistoryLog> GetPagedLogs(int i, int count, DateTime fromDate, DateTime toDate)
        {
            return _set.Where(l => l.DateTime >= fromDate && l.DateTime <= toDate ).OrderByDescending(l => l.DateTime).Skip(i * count).Take(count).ToList();
        }

        public List<HistoryLog> GetPagedLogsFor(int i, int count, string username, DateTime fromDate, DateTime toDate)
        {
            return _set.Where(l => l.Username == username && l.DateTime >= fromDate && l.DateTime <= toDate ).OrderByDescending(l => l.DateTime).Skip(i * count).Take(count).ToList();
        }

        public long GetLogsCount(DateTime fromDate, DateTime toDate)
        {
            return _set.Count(l => l.DateTime >= fromDate && l.DateTime <= toDate);
        }

        public long GetLogsCountFor(string username, DateTime fromDate, DateTime toDate)
        {
            return _set.Count(l => l.Username == username && l.DateTime >= fromDate && l.DateTime <= toDate);
        }
    }
}
