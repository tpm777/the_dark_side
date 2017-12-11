Imports Microsoft.Practices.Unity.InterceptionExtension
Imports System.Reflection
Imports WWTS.Aspects.Enums
Imports WWTS.Aspects.Utils
Imports WWTS.Aspects.Factory

Namespace Behaviors
    Public Class CustomPolicyBehavior
        Implements ICallHandler

        Public Function Invoke(input As IMethodInvocation, getNext As GetNextHandlerDelegate) As IMethodReturn Implements ICallHandler.Invoke
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

        Public Property Order As Integer Implements ICallHandler.Order

        Private Shared Function GetMethodInputs(input As IMethodInvocation) As String
            Dim arguments = String.Empty
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
            Return arguments
        End Function
    End Class


End Namespace

