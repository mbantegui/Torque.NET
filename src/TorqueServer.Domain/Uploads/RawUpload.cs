using System;

namespace TorqueServer.Domain.Uploads
{
    public class RawUpload
    {
        public string EmailAddress { get; set; }
        public long SessionID { get; set; }
        public string CorrelationID { get; set; }
        public DateTime RecordedOn { get; set; }
    }
}