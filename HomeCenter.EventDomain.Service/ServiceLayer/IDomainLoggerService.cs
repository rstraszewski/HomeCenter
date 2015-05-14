using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HomeCenter.HistoryDomain
{
    public interface IDomainLoggerService
    {
        void Publish(string username, DateTime dateTime, string description);
        void Publish(string username, string description);
        List<HistoryLogDto> GetLogs(string username);
        List<HistoryLogDto> GetLogs(int page, int count, string username, long? fromDateTime, long? toDateTime);

        long GetLogsCountForUser(string username, long? fromDateTime, long? toDateTime);
    }
}
