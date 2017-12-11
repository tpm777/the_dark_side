Imports WWTS.Aspects.Enums
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.Logging
Imports System.Runtime.Remoting.Messaging
Imports System.Messaging
Imports WWTS.Aspects.Utils
Imports Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners

Namespace Factory
    Public NotInheritable Class LogTraceFactory
        Private Shared defaultLogWriter As LogWriter
        Private Delegate Sub WriteLogWithCategoryAsync(logMessage As String, category As LogTraceCategoryNames)
        Private Shared sync As New Object()
        Private Shared queuePath As String = String.Empty

        Public Shared Sub InitializeLoggingService()
            Try
                'queuePath = @"FormatName:DIRECT= TCP:192.168.11.89\LogPublicQueue";
                queuePath = XmlTextSerializer.GetAppSettings(ConfigKeys.MSMQPath)
                DatabaseFactory.SetDatabaseProviderFactory(New DatabaseProviderFactory())
                Dim factory As New LogWriterFactory()
                'get the immediate application log writer and trace manager instance from 
                defaultLogWriter = factory.Create()
                Logger.SetLogWriter(defaultLogWriter)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        ''' <summary>
        ''' Method to log messages with log category mentioned
        ''' </summary>
        ''' <param name="message">message to log</param>
        ''' <param name="category">log category</param>
        Public Shared Sub LogAsync(message As String, category As LogTraceCategoryNames)
            Dim logDefinition As New WriteLogWithCategoryAsync(AddressOf WriteLogWithCategory)
            Dim logDefinitionWithCategoryCompletedCallBack__1 As New AsyncCallback(AddressOf LogDefinitionWithCategoryCompletedCallBack)
            SyncLock sync
                logDefinition.BeginInvoke(message, category, logDefinitionWithCategoryCompletedCallBack__1, Nothing)
            End SyncLock
        End Sub

        ''' <summary>
        ''' Method to write log information on the basis of category and message provided
        ''' </summary>
        ''' <param name="logMessage">log message</param>
        ''' <param name="category">log category</param>
        Public Shared Sub WriteLogWithCategory(logMessage As String, category As LogTraceCategoryNames)
            If defaultLogWriter IsNot Nothing Then
                If category = LogTraceCategoryNames.MSMQ Then
                    Dim listner = defaultLogWriter.TraceSources("MSMQ").Listeners.ToList()(0)
                    Dim s As String = DirectCast(listner, FormattedTraceListenerBase).Formatter.Format(New LogEntry() With { _
                         .Message = logMessage _
                    })
                    'LogEntry entry = new LogEntry();
                    'entry.Message = logMessage + " using MSMQ";
                    'Logger.Write(entry);
                    'This code is written because using ELB 6 messages are not getting logged
                    Dim msgQ As New MessageQueue(queuePath)
                    msgQ.Send(logMessage)
                Else
                    defaultLogWriter.Write(logMessage, category.ToString())

                End If
            End If
        End Sub

        ''' <summary>
        ''' Method to handle the log with category completed call back method
        ''' </summary>
        ''' <param name="result">result obtained</param>
        Private Shared Sub LogDefinitionWithCategoryCompletedCallBack(result As IAsyncResult)
            Dim importStock As WriteLogWithCategoryAsync = DirectCast(DirectCast(result, AsyncResult).AsyncDelegate, WriteLogWithCategoryAsync)
        End Sub

    End Class
End Namespace

