using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Web.Http.Metadata.Providers;
using System.Web.Http.ModelBinding;
using TorqueServer.Domain.Uploads;
using TorqueServer.Web.Models;
using Xunit;

namespace TorqueServer.Web.Tests.Models
{
    public static class RawUploadModelBinderTests
    {
        public class BindModelFacts
        {
            [Fact]
            public void ReturnsFalseIfTheModelTypeIsNotRawUpload()
            {
                // Arrange
                var rawUploadModelBinder = new RawUploadModelBinder();

                var modelMetadata = new ModelMetadata(new EmptyModelMetadataProvider(), typeof(object), null, typeof(object), null);
                var modelBindingContext = new ModelBindingContext { ModelMetadata = modelMetadata };

                // Act
                var isBound = rawUploadModelBinder.BindModel(new HttpActionContext(), modelBindingContext);

                // Assert
                Assert.False(isBound);
            }

            [Fact]
            public void ReturnTrueIfTheModelTypeIsRawUpload()
            {
                // Arrange
                var rawUploadModelBinder = new RawUploadModelBinder();

                var modelMetadata = new ModelMetadata(new EmptyModelMetadataProvider(), typeof(object), null, typeof(RawUpload), null);
                var modelBindingContext = new ModelBindingContext { ModelMetadata = modelMetadata };

                // Act
                var isBound = rawUploadModelBinder.BindModel(new HttpActionContext(), modelBindingContext);

                // Assert
                Assert.True(isBound);
            }

            [Fact]
            public void SetsTheBindingContextModelToAnInstanceOfRawUpload()
            {
                // Arrange
                var rawUploadModelBinder = new RawUploadModelBinder();

                var modelMetadata = new ModelMetadata(new EmptyModelMetadataProvider(), typeof(object), null, typeof(RawUpload), null);
                var modelBindingContext = new ModelBindingContext { ModelMetadata = modelMetadata };

                // Act
                rawUploadModelBinder.BindModel(new HttpActionContext(), modelBindingContext);

                // Assert
                Assert.IsType<RawUpload>(modelBindingContext.Model);
            }

            [Fact]
            public void SetsTheEmailAddressPropertyToQueryStringValue()
            {
                // Arrange
                var rawUploadModelBinder = new RawUploadModelBinder();

                var httpRequestMessage = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://localhost/api/Upload?eml=email@test.com")
                };

                var httpActionContext = new HttpActionContext
                {
                    ControllerContext = new HttpControllerContext
                    {
                        Request = httpRequestMessage
                    }
                };

                var modelMetadata = new ModelMetadata(new EmptyModelMetadataProvider(), typeof(object), null, typeof(RawUpload), null);
                var modelBindingContext = new ModelBindingContext { ModelMetadata = modelMetadata };

                // Act
                rawUploadModelBinder.BindModel(httpActionContext, modelBindingContext);

                // Assert
                var model = (RawUpload) modelBindingContext.Model;
                Assert.Equal(model.EmailAddress, "email@test.com");
            }
        }
    }
}