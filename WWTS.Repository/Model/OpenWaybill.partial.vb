Imports WWTS.DataEngine.Contracts

Partial Public Class OpenWaybill
    Implements IKeyedModel(Of Integer)


    Public ReadOnly Property Key As Integer Implements IKeyedModel(Of Integer).Key
        Get
            Return Me.rid
        End Get
    End Property
End Class
