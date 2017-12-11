Imports WWTS.Business.Contracts
Imports Microsoft.Practices.Unity
Imports WWTS.DataEngine.Contracts

Public Class ManagerBase
    Implements IManagerBase
    <Dependency> _
    Public Property TransactionManager() As IRepositoryTransactionManager Implements IManagerBase.TransactionManager
        Get
            Return m_TransactionManager
        End Get
        Set(value As IRepositoryTransactionManager)
            m_TransactionManager = value
        End Set
    End Property
    Private m_TransactionManager As IRepositoryTransactionManager

End Class
