﻿using System;
using System.Collections.Generic;
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
            public void ReturnsFalseIfTheVersionQueryStringParameterIsNotEqualToSeven()
            {
                // Arrange
                var rawUploadModelBinder = new RawUploadModelBinder();

                var httpActionContext = new HttpActionContext
                {
                    ControllerContext = new HttpControllerContext
                    {
                        Request = new HttpRequestMessage { RequestUri = new Uri("http://localhost/Upload?v=6") }
                    }
                };

                var modelMetadata = new ModelMetadata(new EmptyModelMetadataProvider(), typeof(object), null, typeof(RawUpload), null);
                var modelBindingContext = new ModelBindingContext { ModelMetadata = modelMetadata };

                // Act
                var isBound = rawUploadModelBinder.BindModel(httpActionContext, modelBindingContext);

                // Assert
                Assert.False(isBound);
            }

            [Fact]
            public void ReturnsTrueIfTheModelTypeIsRawUploadAndTheVersionIsEqualToSeven()
            {
                // Arrange
                var rawUploadModelBinder = new RawUploadModelBinder();

                var httpActionContext = new HttpActionContext
                {
                    ControllerContext = new HttpControllerContext
                    {
                        Request = new HttpRequestMessage { RequestUri = new Uri("http://localhost/Upload?v=7" ) }
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
                        Request = new HttpRequestMessage { RequestUri = new Uri("http://localhost/Upload?v=7") }
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
                    RequestUri = new Uri("http://localhost/api/Upload?v=7&eml=email@test.com")
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
                Assert.Equal("email@test.com", model.EmailAddress);
            }

            [Fact]
            public void SetsTheEmailAddressPropertyToNullIfTheQueryStringValueIsNotPresent()
            {
                // Arrange
                var rawUploadModelBinder = new RawUploadModelBinder();

                var httpRequestMessage = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://localhost/api/Upload?v=7")
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
                    RequestUri = new Uri("http://localhost/api/Upload?v=7&session=9223372036853775807")
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
                Assert.Equal(9223372036853775807, model.SessionID);
            }

            [Fact]
            public void SetsTheSessionIDPropertyToZeroIfTheQueryStringValueIsNotPresent()
            {
                // Arrange
                var rawUploadModelBinder = new RawUploadModelBinder();

                var httpRequestMessage = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://localhost/api/Upload?v=7")
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
                    RequestUri = new Uri("http://localhost/api/Upload?v=7&id=015c61e58c1dc0e307ecd1a2f7c75cfd")
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
                Assert.Equal("015c61e58c1dc0e307ecd1a2f7c75cfd", model.CorrelationID);
            }

            [Fact]
            public void SetsTheCorrelationIDPropertyToNullIfTheQueryStringValueIsNotPresent()
            {
                // Arrange
                var rawUploadModelBinder = new RawUploadModelBinder();

                var httpRequestMessage = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://localhost/api/Upload?v=7")
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
                    RequestUri = new Uri("http://localhost/api/Upload?v=7&time=1388586600350")
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
                Assert.Equal(new DateTime(2014, 1, 1, 14, 30, 0, 350, DateTimeKind.Utc), model.RecordedOn);
            }

            [Fact]
            public void SetsTheRecordedOnPropertyToTheLinuxEpochStartTimeValueIfTheQueryStringValueIsNotPresent()
            {
                // Arrange
                var rawUploadModelBinder = new RawUploadModelBinder();

                var httpRequestMessage = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://localhost/api/Upload?v=7")
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
                Assert.Equal(new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), model.RecordedOn);
            }

            [Fact]
            public void SetsTheReadingsPropertyToTheKPrefixedQueryStringValues()
            {
                // Arrange
                var rawUploadModelBinder = new RawUploadModelBinder();

                var httpRequestMessage = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://localhost/api/Upload?v=7&kff1005=-73&kff1006=40&asdf=foo")
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
                Assert.Equal(new Dictionary<string, double>
                {
                    { "kff1005", -73.0 },
                    { "kff1006", 40.0 }
                }, model.Readings);
            }
        }
    }
}