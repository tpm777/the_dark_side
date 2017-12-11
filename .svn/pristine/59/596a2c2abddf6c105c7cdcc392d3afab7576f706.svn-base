Imports System.Threading.Tasks

Public Class AsyncBootstrapper
    Implements IDisposable

    Private _webServiceHost As WWTSWebServiceHost
    Private _isStopping As Boolean

    Public Property OnStarting() As Action
        Get
            Return m_OnStarting
        End Get
        Set(value As Action)
            m_OnStarting = value
        End Set
    End Property
    Private m_OnStarting As Action
    Public Property OnStarted() As Action
        Get
            Return m_OnStarted
        End Get
        Set(value As Action)
            m_OnStarted = value
        End Set
    End Property
    Private m_OnStarted As Action
    Public Property OnStopping() As Action
        Get
            Return m_OnStopping
        End Get
        Set(value As Action)
            m_OnStopping = value
        End Set
    End Property
    Private m_OnStopping As Action
    Public Property OnStopped() As Action
        Get
            Return m_OnStopped
        End Get
        Set(value As Action)
            m_OnStopped = value
        End Set
    End Property
    Private m_OnStopped As Action

    ''' <summary>
    ''' Initializes a new instance of the <see cref="AsyncBootstrapper"/> class.
    ''' </summary>
    Public Sub New()
        Me.OnStarting = Function()

                        End Function
        Me.OnStarted = Function()

                       End Function
        Me.OnStopping = Function()

                        End Function
        Me.OnStopped = Function()

                       End Function
    End Sub

    Public Sub StartAsync()
        Task.Factory.StartNew(Function()
                                  Try
                                      'Me.OnStarting()

                                      Me._webServiceHost = New WWTSWebServiceHost(New Uri(ConfigurationManager.AppSettings("ServiceUrl")))

                                      Me._webServiceHost.Open()

                                      Me.WriteWebServiceInfoToConsole()

                                      'Me.OnStarted()
                                  Catch exception As Exception
                                      Me.[Stop]()
                                  End Try

                              End Function)
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Me.[Stop]()
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

    Private Sub [Stop]()
        If Me._isStopping Then
            Return
        End If

        Me._isStopping = True

        'Me.OnStopping()

        If Me._webServiceHost IsNot Nothing AndAlso Me._webServiceHost.State = CommunicationState.Opened Then
            Me._webServiceHost.Close()
        End If

        'Me.OnStopped()
    End Sub

    Private Sub WriteWebServiceInfoToConsole()
#If DEBUG Then
        Console.WriteLine("---------------------------------------------------")
        Console.WriteLine("Adresses")
        Console.WriteLine("---------------------------------------------------")
        Console.WriteLine("Rest: " + Me._webServiceHost.RestUri)
        Console.WriteLine("Soap: " + Me._webServiceHost.SoapUri)
        Console.WriteLine("---------------------------------------------------")
        Console.WriteLine()
#End If
    End Sub


End Class
