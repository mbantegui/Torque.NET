using System.Net;
using System.Net.Http;
using Moq;
using TorqueServer.Domain.Uploads;
using TorqueServer.Web.Controllers;
using Xunit;

namespace TorqueServer.Web.Tests.Controllers
{
    public static class UploadControllerTests
    {
        public class GetFacts
        {
            [Fact]
            public void ReturnsAnHttpResponseMesssageForTheRequest()
            {
                // Arrange
                var httpRequestMessage = new HttpRequestMessage();
                var uploadController = new UploadController(new Mock<IUploadTracker>().Object) { Request = httpRequestMessage };

                // Act
                var httpResponseMessage = uploadController.Get(new RawUpload());

                // Assert
                Assert.Equal(httpResponseMessage.RequestMessage, httpRequestMessage);
            }

            [Fact]
            public void ReturnsAnOkHttpStatusCode()
            {
                // Arrange
                var uploadController = new UploadController(new Mock<IUploadTracker>().Object) { Request = new HttpRequestMessage() };

                // Act
                var httpResponseMessage = uploadController.Get(new RawUpload());

                // Assert
                Assert.Equal(httpResponseMessage.StatusCode, HttpStatusCode.OK);
            }

            [Fact]
            public void SavesTheUpload()
            {
                // Arrange
                var uploadTracker = new Mock<IUploadTracker>();
                var uploadController = new UploadController(uploadTracker.Object) { Request = new HttpRequestMessage() };

                // Act
                var upload = new RawUpload();
                uploadController.Get(upload);

                // Assert
                uploadTracker.Verify(tracker => tracker.Save(upload));
            }
        }
    }
}