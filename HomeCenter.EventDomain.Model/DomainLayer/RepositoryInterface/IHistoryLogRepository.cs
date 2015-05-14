using System;
using System.Collections.Generic;
using HomeCenter.CommonDomain;

namespace HomeCenter.HistoryDomain
{
    public interface IHistoryLogRepository: IGenericRepository<HistoryLog>
    {
        void AddLog(HistoryLog historyLog);
        List<HistoryLog> GetAllLogs();
        List<HistoryLog> GetPagedLogs(int i, int count, DateTime fromDate, DateTime toDate);
        long GetLogsCount(DateTime fromDate, DateTime toDate);
        long GetLogsCountFor(string user, DateTime fromDate, DateTime toDate);
        List<HistoryLog> GetPagedLogsFor(int i, int count, string username, DateTime fromDate, DateTime toDate);
    }
}