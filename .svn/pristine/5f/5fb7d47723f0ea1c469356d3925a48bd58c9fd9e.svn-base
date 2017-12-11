Imports Microsoft.Practices.Unity.InterceptionExtension
Imports System.Reflection
Imports WWTS.Aspects.Enums
Imports WWTS.Aspects.Utils
Imports WWTS.Aspects.Factory

Public Class ExceptionBehavior
    Implements IInterceptionBehavior


    Public Function GetRequiredInterfaces() As IEnumerable(Of Type) Implements IInterceptionBehavior.GetRequiredInterfaces
        Return (Type.EmptyTypes)
    End Function

    Public Function Invoke(input As IMethodInvocation, getNext As GetNextInterceptionBehaviorDelegate) As IMethodReturn Implements IInterceptionBehavior.Invoke
        Dim isLogging As Boolean = XmlTextSerializer.GetAppSettings(ConfigKeys.MethodLevelLoggingEnabled) = "1"
        Dim methodName As String = input.MethodBase.Name
        If isLogging Then
            LogTraceFactory.WriteLogWithCategory(String.Format("Method {0} called at time {1}", methodName, DateTime.Now), LogTraceCategoryNames.DiskFiles)
            LogTraceFactory.WriteLogWithCategory(GetMethodInputs(input), LogTraceCategoryNames.DiskFiles)
        End If
        Dim methodReturn As IMethodReturn = getNext().Invoke(input, getNext)
        If methodReturn.Exception IsNot Nothing AndAlso Not methodReturn.Exception.StackTrace.Contains("Microsoft.Practices.EnterpriseLibrary.ExceptionHandling") Then
            ExceptionFactory.AppExceptionManager.HandleException(methodReturn.Exception, ExceptionPolicyNames.AssistingAdministrators.ToString())
        End If

        If isLogging Then
            LogTraceFactory.WriteLogWithCategory(String.Format("Method {0} executed successfully at time {1}", methodName, DateTime.Now), LogTraceCategoryNames.DiskFiles)
        End If
        Return methodReturn
    End Function

    Public ReadOnly Property WillExecute As Boolean Implements IInterceptionBehavior.WillExecute
        Get
            Return True
        End Get
    End Property

    Private Shared Function GetMethodInputs(input As IMethodInvocation) As String
        Dim arguments = String.Empty
        Try
            For i As Integer = 0 To input.Arguments.Count - 1
                Dim paramType = input.Arguments.GetParameterInfo(i).ParameterType.Name.ToLower()

                If Not paramType.StartsWith("obj") Then
                    ' in that case object definition need to start with "Obj*"
                    arguments += input.Arguments.GetParameterInfo(i).Name + " - " + input.Arguments(i) + " | "
                Else
                    For Each value As Object In input.Arguments
                        ' i ??
                        Dim properties As PropertyInfo() = value.[GetType]().GetProperties()

                        arguments += properties.Aggregate(arguments, Function(current, pi) current + (pi.Name + " - " + pi.GetValue(value, Nothing) + " | "))
                    Next
                End If
            Next
        Catch ex As Exception
            ex = Nothing
        End Try
        Return arguments
    End Function
End Class
