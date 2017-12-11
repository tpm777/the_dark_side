Imports WWTS.Business.Contracts

Public Class ServiceBase
    Private m_customerServiceBusinessInstance As ICustomerService

    Public ReadOnly Property CustomerServiceBusinessInstance() As ICustomerService
        Get
            If m_customerServiceBusinessInstance Is Nothing Then
                m_customerServiceBusinessInstance = ServiceBootstrapper.IocEngine.Resolve(Of ICustomerService)()
            End If

            Return m_customerServiceBusinessInstance
        End Get
    End Property

End Class
