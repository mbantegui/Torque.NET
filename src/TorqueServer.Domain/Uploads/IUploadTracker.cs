namespace TorqueServer.Domain.Uploads
{
    public interface IUploadTracker
    {
        void Save(RawUpload rawUpload);
    }
}