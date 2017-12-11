Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports Microsoft.Practices.EnterpriseLibrary.Common.Configuration

Namespace Factory
    Public NotInheritable Class ExceptionFactory
        Private Sub New()
        End Sub

        'private variable for exception manager instance
        Private Shared exceptionManager As ExceptionManager

        ''' <summary>
        ''' Property to get set exception manager instance
        ''' </summary>
        Public Shared Property AppExceptionManager() As ExceptionManager
            Get
                Return exceptionManager
            End Get
            Private Set(value As ExceptionManager)
                exceptionManager = value
            End Set
        End Property

        Public Shared Function Create(format As String, ParamArray args As Object()) As Exception
            Return New Exception(String.Format(format, args))
        End Function

        Public Shared Function Create(innerException As Exception, format As String, ParamArray args As Object()) As Exception
            Return New Exception(String.Format(format, args), innerException)
        End Function

        Public Shared Function Create(Of TException As Exception)(format As String, ParamArray args As Object()) As TException
            Return DirectCast(Activator.CreateInstance(GetType(TException), String.Format(format, args)), TException)
        End Function

        Public Shared Function Create(Of TException As Exception)(innerException As Exception, format As String, ParamArray args As Object()) As TException
            Return DirectCast(Activator.CreateInstance(GetType(TException), String.Format(format, args), innerException), TException)
        End Function

        ''' <summary>
        ''' Method to initialize the enterprise library exception handler instance under the Aop
        ''' </summary>
        Public Shared Sub InitializeExceptionAopFramework()
            Try
                Dim config As IConfigurationSource = ConfigurationSourceFactory.Create()
                Dim factory As New ExceptionPolicyFactory(config)
                config.Dispose()
                exceptionManager = factory.CreateManager()

                ExceptionPolicy.SetExceptionManager(exceptionManager)

            Catch ex As Exception
            End Try
        End Sub


    End Class
End Namespace

