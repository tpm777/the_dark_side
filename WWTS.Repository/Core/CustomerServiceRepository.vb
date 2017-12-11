Imports WWTS.DataEngine.Contracts
Imports WWTS.DataEngine
Imports System.ComponentModel.Composition
Imports System.Data.Entity
Imports System.Linq

<Export(GetType(ICustomerServiceRepository))> _
<PartCreationPolicy(CreationPolicy.NonShared)>
Public Class CustomerServiceRepository
    Inherits EFSynchronousRepository(Of Integer, OpenWaybill, OneviewEntities)
    Implements ICustomerServiceRepository

    Protected Overrides Function GetDbSet(context As OneviewEntities) As DbSet(Of OpenWaybill)
        Return context.OpenWaybills
    End Function

    Protected Overrides Function FindImpl(key As Integer, dbSet As DbSet(Of OpenWaybill)) As OpenWaybill
        Return dbSet.FirstOrDefault(Function(row) row.rid = key)
    End Function

    Protected Overrides Function FindImplWithExpand(key As Integer, dbSet As DbSet(Of OpenWaybill), includeQueryPath As String) As OpenWaybill
        Return dbSet.Include(includeQueryPath).FirstOrDefault(Function(row) row.rid = key)

    End Function
End Class
