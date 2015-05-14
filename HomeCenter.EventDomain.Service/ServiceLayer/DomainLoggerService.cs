using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
namespace HomeCenter.HistoryDomain
{
    public class DomainLoggerService : IDomainLoggerService
    {
        private readonly IHistoryLogRepository _historyLogRepository;

        public DomainLoggerService(IHistoryLogRepository historyLogRepository)
        {
            _historyLogRepository = historyLogRepository;
        }

        public void Publish(string username, DateTime dateTime, string description)
        {
            var log = new HistoryLog() { DateTime = dateTime, Username = username, Description = description };
            _historyLogRepository.AddLog(log);
            _historyLogRepository.Save();
        }

        public void Publish(string username, string description)
        {
            var log = new HistoryLog() { DateTime = DateTime.Now, Username = username, Description = description };
            _historyLogRepository.AddLog(log);
            _historyLogRepository.Save();
        }

        public List<HistoryLogDto> GetLogs(string username)
        {

            return Mapper.Map<List<HistoryLogDto>>(_historyLogRepository.GetAllLogs());
        }

        public List<HistoryLogDto> GetLogs(int page, int count, string username, long? fromDateTime, long? toDateTime)
        {
            var fromDate = DateTime.MinValue;
            var toDate = DateTime.MaxValue;

            if (fromDateTime != null)
                fromDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                    .AddMilliseconds(fromDateTime.Value)
                    .ToLocalTime();

            if (toDateTime != null)
                toDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                    .AddMilliseconds(toDateTime.Value)
                    .ToLocalTime();


            if (string.IsNullOrWhiteSpace(username))
                return Mapper.Map<List<HistoryLogDto>>(_historyLogRepository.GetPagedLogs(page - 1, count, fromDate, toDate));
            else
            {
                return Mapper.Map<List<HistoryLogDto>>(_historyLogRepository.GetPagedLogsFor(page - 1, count, username, fromDate, toDate));
            }
        }

        public long GetLogsCountForUser(string username, long? fromDateTime, long? toDateTime)
        {
            var fromDate = DateTime.MinValue;
            var toDate = DateTime.MaxValue;

            if (fromDateTime != null)
                fromDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                    .AddMilliseconds(fromDateTime.Value)
                    .ToLocalTime();

            if (toDateTime != null)
                toDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                    .AddMilliseconds(toDateTime.Value)
                    .ToLocalTime();

            if (string.IsNullOrWhiteSpace(username))
                return _historyLogRepository.GetLogsCount(fromDate, toDate);
            else
            {
                return _historyLogRepository.GetLogsCountFor(username,fromDate, toDate);
            }
        }
    }
}
