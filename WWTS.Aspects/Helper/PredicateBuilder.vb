Imports System.Linq.Expressions

Namespace Helper
    ''' <summary>  
    ''' Enables the efficient, dynamic composition of query predicates.  
    ''' </summary>  
    Public NotInheritable Class PredicateBuilder
        Private Sub New()
        End Sub

        ''' <summary>  
        ''' Creates a predicate that evaluates to true.  
        ''' </summary>  
        Public Shared Function [True](Of T)() As Expression(Of Func(Of T, Boolean))
            Return Function(param) True
        End Function

        ''' <summary>  
        ''' Creates a predicate that evaluates to false.  
        ''' </summary>  
        Public Shared Function [False](Of T)() As Expression(Of Func(Of T, Boolean))
            Return Function(param) False
        End Function

        ''' <summary>  
        ''' Creates a predicate expression from the specified lambda expression.  
        ''' </summary>  
        Public Shared Function Create(Of T)(predicate As Expression(Of Func(Of T, Boolean))) As Expression(Of Func(Of T, Boolean))
            Return predicate
        End Function

        

    End Class

    Public Module LinqExtension
        ''' <summary>  
        ''' Combines the first predicate with the second using the logical "and".  
        ''' </summary>  
        <System.Runtime.CompilerServices.Extension> _
        Public Function [And](Of T)(first As Expression(Of Func(Of T, Boolean)), second As Expression(Of Func(Of T, Boolean))) As Expression(Of Func(Of T, Boolean))
            Return first.Compose(second, Expression.[AndAlso])
        End Function

        ''' <summary>  
        ''' Combines the first predicate with the second using the logical "or".  
        ''' </summary>  
        <System.Runtime.CompilerServices.Extension> _
        Public Function [Or](Of T)(first As Expression(Of Func(Of T, Boolean)), second As Expression(Of Func(Of T, Boolean))) As Expression(Of Func(Of T, Boolean))
            Return first.Compose(second, Expression.[OrElse])
        End Function

        ''' <summary>  
        ''' Negates the predicate.  
        ''' </summary>  
        <System.Runtime.CompilerServices.Extension> _
        Public Function [Not](Of T)(expression__1 As Expression(Of Func(Of T, Boolean))) As Expression(Of Func(Of T, Boolean))
            Dim negated = Expression.[Not](expression__1.Body)
            Return Expression.Lambda(Of Func(Of T, Boolean))(negated, expression__1.Parameters)
        End Function

        ''' <summary>  
        ''' Combines the first expression with the second using the specified merge function.  
        ''' </summary>  
        <System.Runtime.CompilerServices.Extension> _
        Private Function Compose(Of T)(first As Expression(Of T), second As Expression(Of T), merge As Func(Of Expression, Expression, Expression)) As Expression(Of T)
            ' zip parameters (map from parameters of second to parameters of first)  
            Dim map = first.Parameters.[Select](Function(f, i) New With { _
                f, _
                Key .s = second.Parameters(i) _
            }).ToDictionary(Function(p) p.s, Function(p) p.f)

            ' replace parameters in the second lambda expression with the parameters in the first  
            Dim secondBody = ParameterRebinder.ReplaceParameters(map, second.Body)

            ' create a merged lambda expression with parameters from the first expression  
            Return Expression.Lambda(Of T)(merge(first.Body, secondBody), first.Parameters)
        End Function

    End Module

    Class ParameterRebinder
        Inherits ExpressionVisitor
        ReadOnly map As Dictionary(Of ParameterExpression, ParameterExpression)

        Private Sub New(map As Dictionary(Of ParameterExpression, ParameterExpression))
            Me.map = If(map, New Dictionary(Of ParameterExpression, ParameterExpression)())
        End Sub

        Public Shared Function ReplaceParameters(map As Dictionary(Of ParameterExpression, ParameterExpression), exp As Expression) As Expression
            Return New ParameterRebinder(map).Visit(exp)
        End Function

        Protected Overrides Function VisitParameter(p As ParameterExpression) As Expression
            Dim replacement As ParameterExpression

            If map.TryGetValue(p, replacement) Then
                p = replacement
            End If

            Return MyBase.VisitParameter(p)
        End Function
    End Class
End Namespace

