Imports WWTC.IocEngine
Imports WWTS.Aspects.Factory

Public Class ServiceBootstrapper
    Public Shared Property IocEngine() As IIocManager
        Get
            Return m_IocEngine
        End Get
        Private Set(value As IIocManager)
            m_IocEngine = value
        End Set
    End Property
    Private Shared m_IocEngine As IIocManager

    Public Shared Sub Initialize()
        IocEngine = New IocManager(DiscoveryStrategy.SearchBaseDirectory, "WWTS")
        LogTraceFactory.InitializeLoggingService()
        ExceptionFactory.InitializeExceptionAopFramework()

        InitializeEntityModelMapping()
    End Sub

    Private Shared Sub InitializeEntityModelMapping()
    End Sub
End Class
