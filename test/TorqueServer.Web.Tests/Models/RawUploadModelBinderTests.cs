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

                var httpActionContext = new HttpActionContext
                {
                    ControllerContext = new HttpControllerContext
                    {
                        Request = new HttpRequestMessage()
                    }
                };

                var modelMetadata = new ModelMetadata(new EmptyModelMetadataProvider(), typeof(object), null, typeof(RawUpload), null);
                var modelBindingContext = new ModelBindingContext { ModelMetadata = modelMetadata };

                // Act
                var isBound = rawUploadModelBinder.BindModel(httpActionContext, modelBindingContext);

                // Assert
                Assert.True(isBound);
            }

            [Fact]
            public void SetsTheBindingContextModelToAnInstanceOfRawUpload()
            {
                // Arrange
                var rawUploadModelBinder = new RawUploadModelBinder();

                var httpActionContext = new HttpActionContext
                {
                    ControllerContext = new HttpControllerContext
                    {
                        Request = new HttpRequestMessage()
                    }
                };
                var modelMetadata = new ModelMetadata(new EmptyModelMetadataProvider(), typeof(object), null, typeof(RawUpload), null);
                var modelBindingContext = new ModelBindingContext { ModelMetadata = modelMetadata };

                // Act
                rawUploadModelBinder.BindModel(httpActionContext, modelBindingContext);

                // Assert
                Assert.IsType<RawUpload>(modelBindingContext.Model);
            }

            [Fact]
            public void SetsTheEmailAddressPropertyToTheQueryStringValue()
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

            [Fact]
            public void SetsTheEmailAddressPropertyToNullIfTheQueryStringValueIsNotPresent()
            {
                // Arrange
                var rawUploadModelBinder = new RawUploadModelBinder();

                var httpRequestMessage = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://localhost/api/Upload")
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
                var model = (RawUpload)modelBindingContext.Model;
                Assert.Null(model.EmailAddress);
            }

            [Fact]
            public void SetsTheSessionIDPropertyToTheQueryStringValue()
            {
                // Arrange
                var rawUploadModelBinder = new RawUploadModelBinder();

                var httpRequestMessage = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://localhost/api/Upload?session=9223372036853775807")
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
                var model = (RawUpload)modelBindingContext.Model;
                Assert.Equal(model.SessionID, 9223372036853775807);
            }

            [Fact]
            public void SetsTheSessionIDPropertyToZeroIfTheQueryStringValueIsNotPresent()
            {
                // Arrange
                var rawUploadModelBinder = new RawUploadModelBinder();

                var httpRequestMessage = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://localhost/api/Upload")
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
                var model = (RawUpload)modelBindingContext.Model;
                Assert.Equal(model.SessionID, 0);
            }

            [Fact]
            public void SetsTheCorrelationIDPropertyToTheQueryStringValue()
            {
                // Arrange
                var rawUploadModelBinder = new RawUploadModelBinder();

                var httpRequestMessage = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://localhost/api/Upload?id=015c61e58c1dc0e307ecd1a2f7c75cfd")
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
                var model = (RawUpload)modelBindingContext.Model;
                Assert.Equal(model.CorrelationID, "015c61e58c1dc0e307ecd1a2f7c75cfd");
            }

            [Fact]
            public void SetsTheCorrelationIDPropertyToNullIfTheQueryStringValueIsNotPresent()
            {
                // Arrange
                var rawUploadModelBinder = new RawUploadModelBinder();

                var httpRequestMessage = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://localhost/api/Upload")
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
                var model = (RawUpload)modelBindingContext.Model;
                Assert.Null(model.CorrelationID);
            }

            [Fact]
            public void SetsTheRecordedOnPropertyToTheQueryStringValue()
            {
                // Arrange
                var rawUploadModelBinder = new RawUploadModelBinder();

                var httpRequestMessage = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://localhost/api/Upload?time=1388586600")
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
                var model = (RawUpload)modelBindingContext.Model;
                Assert.Equal(new DateTime(2014, 1, 1, 14, 30, 0, 0, DateTimeKind.Utc), model.RecordedOn);
            }

            [Fact]
            public void SetsTheRecordedOnPropertyToTheMinDateTimeValueIfTheQueryStringValueIsNotPresent()
            {
                // Arrange
                var rawUploadModelBinder = new RawUploadModelBinder();

                var httpRequestMessage = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://localhost/api/Upload")
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
                var model = (RawUpload)modelBindingContext.Model;
                Assert.Equal(model.RecordedOn, DateTime.MinValue);
            }
        }
    }
}